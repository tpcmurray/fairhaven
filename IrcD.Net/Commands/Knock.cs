using System.Collections.Generic;
using System.Linq;
using IrcD.Channel;

namespace IrcD.Commands
{
    public class KnockArgument : CommandArgument
    {
        public KnockArgument(UserInfo sender, InfoBase receiver, ChannelInfo channel, string message)
            : base(sender, receiver, "KNOCK")
        {
            Channel = channel;
            Message = message;
        }

        public ChannelInfo Channel { get; private set; }
        public string Message { get; private set; }
    }
    
    public class Knock : CommandBase
    {
        public Knock(IrcDaemon ircDaemon)
            : base(ircDaemon, "KNOCK", "")
        { }

        [CheckRegistered]
        [CheckParamCount(1)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            ChannelInfo chan;
            if(IrcDaemon.Channels.TryGetValue(args[0], out chan))
            {
                if(!chan.Modes.HandleEvent(this, chan, info, args))
                {
                    return;
                }
                Send(new NoticeArgument(null, chan, chan.Name, "[KNOCK] by " + info.Usermask + "(" + ((args.Count > 1) ? args[1] : "no reason specified") + ")"));
                Send(new NoticeArgument(null, info, info.Nick, "Knocked on " + chan.Name));
            }
            else
            {
                IrcDaemon.Replies.SendNoSuchChannel(info, args[0]);
            }
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            var arg = GetSaveArgument<KnockArgument>(commandArgument);
            BuildMessageHeader(arg);
            Command.Append(arg.Channel.Name);
            Command.Append(" :");
            Command.Append(arg.Message);
            return arg.Receiver.WriteLine(Command);
        }

        public override IEnumerable<string> Support(IrcDaemon ircDaemon)
        {
            return Enumerable.Repeat("KNOCK", 1);
        }
    }
}
