using System;
using System.Collections.Generic;
using IrcD.Modes.UserModes;

namespace IrcD.Commands
{
    public class AwayArgument : CommandArgument
    {
        public AwayArgument(UserInfo sender, InfoBase receiver, string awayMessage)
            : base(sender, receiver, "AWAY")
        {
            AwayMessage = awayMessage;
        }
        public string AwayMessage { get; private set; }
    }

    public class Away : CommandBase
    {
        public Away(IrcDaemon ircDaemon)
            : base(ircDaemon, "AWAY", "A")
        {
            if(!ircDaemon.Capabilities.Contains("away-notify"))
            {
                ircDaemon.Capabilities.Add("away-notify");
            }
        }

        [CheckRegistered]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            if(args.Count == 0)
            {
                info.AwayMessage = null;
                info.Modes.RemoveMode<ModeAway>();
                IrcDaemon.Replies.SendUnAway(info);
            }
            else
            {
                info.AwayMessage = args[0];
                info.Modes.Add(new ModeAway());
                IrcDaemon.Replies.SendNowAway(info);
            }

            foreach(var channel in info.Channels)
            {
                foreach(var user in channel.Users)
                {
                    if(user.Capabilities.Contains("away-notify"))
                    {

                        Send(new AwayArgument(info, user, (args.Count == 0) ? null : args[0]));
                    }
                }
            }
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            var arg = GetSaveArgument<AwayArgument>(commandArgument);
            BuildMessageHeader(arg);

            if(arg.AwayMessage != null)
            {
                Command.Append(arg.AwayMessage);
            }
            return arg.Receiver.WriteLine(Command);
        }
    }
}
