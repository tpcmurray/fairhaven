using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class Error : CommandBase
    {
        public Error(IrcDaemon ircDaemon)
            : base(ircDaemon, "ERROR", "Y")
        { }

        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}