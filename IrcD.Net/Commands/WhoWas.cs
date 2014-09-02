using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class WhoWas : CommandBase
    {
        public WhoWas(IrcDaemon ircDaemon)
            : base(ircDaemon, "WHOWAS", "X")
        { }

        [CheckRegistered]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}