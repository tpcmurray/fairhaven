using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class Trace : CommandBase
    {
        public Trace(IrcDaemon ircDaemon)
            : base(ircDaemon, "TRACE", "TR")
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