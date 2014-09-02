using System.Collections.Generic;
using System.Linq;

namespace IrcD.Commands
{
    public class ModeArgument : CommandArgument
    {
        public ModeArgument(UserInfo sender, InfoBase receiver, string target, string modeString)
            : base(sender, receiver, "MODE")
        {
            Target = target;
            ModeString = modeString;
        }

        public string Target { get; private set; }
        public string ModeString { get; private set; }
    }

    public class Mode : CommandBase
    {
        public Mode(IrcDaemon ircDaemon)
            : base(ircDaemon, "MODE", "M")
        { }

        [CheckRegistered]
        [CheckParamCount(1)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            // Check if its a channel
            if(IrcDaemon.ValidChannel(args[0]))
            {
                if(!IrcDaemon.Channels.ContainsKey(args[0]))
                {
                    IrcDaemon.Replies.SendNoSuchChannel(info, args[0]);
                    return;
                }
                var chan = IrcDaemon.Channels[args[0]];
                // Modes command without any mode -> query the Mode of the Channel
                if(args.Count == 1)
                {
                    IrcDaemon.Replies.SendChannelModeIs(info, chan);
                    return;
                }
                // Update the Channel Modes
                chan.Modes.Update(info, chan, args.Skip(1));
            }
            else if(args[0] == info.Nick)
            {
                // Modes command without any mode -> query the Mode of the User
                if(args.Count == 1)
                {
                    IrcDaemon.Replies.SendUserModeIs(info);
                    return;
                }
                // Update the User Modes
                info.Modes.Update(info, args.Skip(1));
            }
            else
            {
                // You cannot use Mode on any user but yourself
                IrcDaemon.Replies.SendUsersDoNotMatch(info);
            }
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            var arg = GetSaveArgument<ModeArgument>(commandArgument);

            BuildMessageHeader(arg);
            Command.Append(arg.Target);
            Command.Append(" ");
            Command.Append(arg.ModeString);
            return arg.Receiver.WriteLine(Command);
        }
    }
}