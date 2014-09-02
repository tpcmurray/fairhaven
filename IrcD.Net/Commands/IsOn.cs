using System;
using System.Collections.Generic;
using System.Linq;

namespace IrcD.Commands
{
    public class IsOn : CommandBase
    {
        public IsOn(IrcDaemon ircDaemon)
            : base(ircDaemon, "ISON", "ISON")
        { }

        [CheckRegistered]
        [CheckParamCount(1)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            IrcDaemon.Replies.SendIsOn(info, args.Where(nick => IrcDaemon.Nicks.ContainsKey(nick)));
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}