using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class ServiceList : CommandBase
    {
        public ServiceList(IrcDaemon ircDaemon)
            : base(ircDaemon, "SERVLIST", "SERVLIST")
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