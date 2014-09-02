using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class Names : CommandBase
    {
        public Names(IrcDaemon ircDaemon)
            : base(ircDaemon, "NAMES", "E")
        { }

        [CheckRegistered]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            if(args.Count < 1)
            {
                // TODO: list all visible users
                return;
            }
            //TODO: taget parameter
            foreach(var ch in GetSubArgument(args[0]))
            {
                if(IrcDaemon.Channels.ContainsKey(ch))
                {
                    IrcDaemon.Replies.SendNamesReply(info, IrcDaemon.Channels[ch]);
                    IrcDaemon.Replies.SendEndOfNamesReply(info, IrcDaemon.Channels[ch]);
                }
            }
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}