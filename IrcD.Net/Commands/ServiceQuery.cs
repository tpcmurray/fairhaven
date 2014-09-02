using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class ServiceQuery : CommandBase
    {
        public ServiceQuery(IrcDaemon ircDaemon)
            : base(ircDaemon, "SQUERY", "SQUERY")
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