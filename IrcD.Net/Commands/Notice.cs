using System.Collections.Generic;

namespace IrcD.Commands
{
    public class NoticeArgument : CommandArgument
    {
        public NoticeArgument(UserInfo sender, InfoBase receiver, string target, string message)
            : base(sender, receiver, "NOTICE")
        {
            Target = target;
            Message = message;
        }

        public string Target { get; private set; }
        public string Message { get; private set; }
    }
    
    public class Notice : CommandBase
    {
        public Notice(IrcDaemon ircDaemon)
            : base(ircDaemon, "NOTICE", "O")
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
                    Send(new NoticeArgument(info, chan, chan.Name, args[1]));
                }
                else
                {
                    IrcDaemon.Replies.SendCannotSendToChannel(info, args[0]);
                }
            }
            else if(IrcDaemon.ValidNick(args[0]))
            {
                if(IrcDaemon.Nicks.ContainsKey(args[0]))
                {
                    var user = IrcDaemon.Nicks[args[0]];
                    if(user.AwayMessage != null)
                    {
                        IrcDaemon.Replies.SendAwayMessage(info, user);
                    }
                    // Send PM
                    Send(new NoticeArgument(info, user, user.Nick, args[1]));
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
            var arg = GetSaveArgument<NoticeArgument>(commandArgument);

            BuildMessageHeader(arg);
            Command.Append(arg.Target);
            Command.Append(" :");
            Command.Append(arg.Message);
            if(arg.Sender == null)
            {
                return arg.Receiver.WriteLine(Command);
            }
            else
            {
                return arg.Receiver.WriteLine(Command, arg.Sender);
            }
        }
    }
}