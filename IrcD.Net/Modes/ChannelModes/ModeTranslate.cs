﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using IrcD.Channel;
using IrcD.Commands;
using IrcD.Utils;

namespace IrcD.Modes.ChannelModes
{
    class ModeTranslate : ChannelMode
    {
        private readonly GoogleTranslate translator;
        public ModeTranslate()
            : base('T')
        {
            translator = new GoogleTranslate();
        }

        private bool onlyOnce;
        public override bool HandleEvent(CommandBase command, ChannelInfo channel, UserInfo user, List<string> args)
        {
            if(onlyOnce) { return true; }
            onlyOnce = true;
            if(command is Join)
            {
                user.IrcDaemon.Commands.Send(new NoticeArgument(user, user, channel.Name, "This channel automatically translates your messages, use the LANGUAGE command to set your preferred language"));
            }
            if(!channel.Modes.HandleEvent(command, channel, user, args))
            {
                onlyOnce = false;
                return false;
            }
            if(command is PrivateMessage || command is Notice)
            {
                var translateDelegate = new GoogleTranslate.TranslateMultipleDelegate(translator.TranslateText);
                translateDelegate.BeginInvoke(args[1], channel.Users.Select(u => u.Languages.First()).Distinct(), TranslateCallBack, Tuple.Create(channel, user, command));
                onlyOnce = false;
                return false;
            }
            onlyOnce = false;
            return true;
        }

        private static void TranslateCallBack(IAsyncResult asyncResult)
        {
            var state = (Tuple<ChannelInfo, UserInfo, CommandBase>)asyncResult.AsyncState;
            var asyncDelegate = ((AsyncResult)asyncResult).AsyncDelegate;
            var result = ((GoogleTranslate.TranslateMultipleDelegate)asyncDelegate).EndInvoke(asyncResult);
            foreach(var user in state.Item1.Users.Where(u => u != state.Item2))
            {
                Tuple<string, string, string> res;
                string message;
                if(user.Languages.Contains(result[GoogleTranslate.Original].Item1))
                {
                    message = "[" + result[GoogleTranslate.Original].Item1 + "] " + result[GoogleTranslate.Original].Item3;
                }
                else if(result.TryGetValue(user.Languages.First(), out res))
                {
                    message = "[" + res.Item1 + "] " + res.Item3;
                }
                else if(result.Any())
                {
                    message = "[" + result.Last().Value.Item1 + "] " + result.Last().Value.Item3;
                }
                else
                {
                    // This should never happen: There must be always at least the Original in the result
                    message = "BUG: Translation failed miserably";
                }
                if(state.Item3 is PrivateMessage)
                {
                    user.IrcDaemon.Commands.Send(new PrivateMessageArgument(state.Item2, user, state.Item1.Name, message));
                }
                if(state.Item3 is Notice)
                {
                    user.IrcDaemon.Commands.Send(new PrivateMessageArgument(state.Item2, user, state.Item1.Name, message));
                }
            }
        }
    }
}