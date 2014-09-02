using System;
using System.Collections.Generic;
using IrcD.Modes.UserModes;

namespace IrcD.Commands
{
    public class PrivateMessageArgument : CommandArgument
    {
        public PrivateMessageArgument(UserInfo sender, InfoBase receiver, string target, string message)
            : base(sender, receiver, "PRIVMSG")
        {
            Target = target;
            Message = message;
        }

        public string Target { get; private set; }
        public string Message { get; private set; }
    }
    
    public class PrivateMessage : CommandBase
    {
        public PrivateMessage(IrcDaemon ircDaemon)
            : base(ircDaemon, "PRIVMSG", "P")
        { }

        [CheckRegistered]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            if(args.Count < 1)
            {
                IrcDaemon.Replies.SendNoRecipient(info, Name);
                return;
            }
            if(args.Count < 2)
            {
                IrcDaemon.Replies.SendNoTextToSend(info);
                return;
            }
            // Only Private Messages set this
            info.LastAction = DateTime.Now;
            if(IrcDaemon.ValidChannel(args[0]))
            {
                if(IrcDaemon.Channels.ContainsKey(args[0]))
                {
                    var chan = IrcDaemon.Channels[args[0]];
                    if(!chan.Modes.HandleEvent(this, chan, info, args))
                    {
                        return;
                    }
                    // Send Channel Message
                    Send(new PrivateMessageArgument(info, chan, chan.Name, args[1]));
                }
                else
                {
                    IrcDaemon.Replies.SendCannotSendToChannel(info, args[0]);
                }
            }
            else if(IrcDaemon.ValidNick(args[0]))
            {
                UserInfo user;
                if(IrcDaemon.Nicks.TryGetValue(args[0], out user))
                {
                    if(user.Modes.Exist<ModeAway>())
                    {
                        IrcDaemon.Replies.SendAwayMessage(info, user);
                    }
                    // Send Private Message
                    Send(new PrivateMessageArgument(info, user, user.Nick, args[1]));
                }
                else
                {
                    IrcDaemon.Replies.SendNoSuchNick(info, args[0]);
                }
            }
            else
            {
                IrcDaemon.Replies.SendNoSuchNick(info, args[0]);
            }
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            var arg = GetSaveArgument<PrivateMessageArgument>(commandArgument);
            BuildMessageHeader(arg);
            Command.Append(arg.Target);
            Command.Append(" :");
            Command.Append(arg.Message);
            return arg.Receiver.WriteLine(Command, arg.Sender);
        }
    }
}