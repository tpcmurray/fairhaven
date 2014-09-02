using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class ServerQuit : CommandBase
    {
        public ServerQuit(IrcDaemon ircDaemon)
            : base(ircDaemon, "SQUIT", "SQ")
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