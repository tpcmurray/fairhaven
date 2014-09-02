using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class Links : CommandBase
    {
        public Links(IrcDaemon ircDaemon)
            : base(ircDaemon, "LINKS", "LI")
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