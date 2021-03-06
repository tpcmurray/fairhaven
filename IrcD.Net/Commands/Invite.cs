﻿using System.Collections.Generic;
using IrcD.Channel;

namespace IrcD.Commands
{
    public class InviteArgument : CommandArgument
    {
        public InviteArgument(UserInfo sender, InfoBase receiver, UserInfo invited, ChannelInfo channel)
            : base(sender, receiver, "INVITE")
        {
            Invited = invited;
            Channel = channel;
        }

        public UserInfo Invited { get; private set; }
        public ChannelInfo Channel { get; private set; }
    }

    public class Invite : CommandBase
    {
        public Invite(IrcDaemon ircDaemon)
            : base(ircDaemon, "INVITE", "I") { }

        [CheckRegistered]
        [CheckParamCount(2)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            UserInfo invited;
            if(!IrcDaemon.Nicks.TryGetValue(args[0], out invited))
            {
                IrcDaemon.Replies.SendNoSuchNick(info, args[0]);
            }
            var channel = args[1];
            ChannelInfo chan;
            if(IrcDaemon.Channels.TryGetValue(channel, out chan))
            {
                if(chan.UserPerChannelInfos.ContainsKey(invited.Nick))
                {
                    IrcDaemon.Replies.SendUserOnChannel(info, invited, chan);
                    return;
                }
                if(!chan.Modes.HandleEvent(this, chan, info, args))
                {
                    return;
                }
                if(!invited.Invited.Contains(chan))
                {
                    invited.Invited.Add(chan);
                }
            }
            //TODO channel does not exist? ... clean up below
            IrcDaemon.Replies.SendInviting(info, invited, channel);
            Send(new InviteArgument(info, invited, invited, chan));
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            var arg = GetSaveArgument<InviteArgument>(commandArgument);

            BuildMessageHeader(arg);
            Command.Append(arg.Invited.Nick);
            Command.Append(" ");
            Command.Append(arg.Channel.Name);
            return arg.Receiver.WriteLine(Command);
        }
    }
}