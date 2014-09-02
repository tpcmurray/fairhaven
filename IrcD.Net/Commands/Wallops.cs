using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class Wallops : CommandBase
    {
        public Wallops(IrcDaemon ircDaemon)
            : base(ircDaemon, "WALLOPS", "WA")
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