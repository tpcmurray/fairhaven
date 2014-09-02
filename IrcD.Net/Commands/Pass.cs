using System;
using System.Collections.Generic;
using System.Linq;

namespace IrcD.Commands
{
    public class Pass : CommandBase
    {
        public Pass(IrcDaemon ircDaemon)
            : base(ircDaemon, "PASS", "PA")
        { }

        [CheckParamCount(1)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            if(info.PassAccepted)
            {
                IrcDaemon.Replies.SendAlreadyRegistered(info);
                return;
            }
            if(args[0] == IrcDaemon.Options.ServerPass)
            {
                info.PassAccepted = true;
                return;
            }
            if(IrcDaemon.Options.ConnectionPasses.Any(p => p == args[0]))
            {
                // This is an allowed Server connection
            }
            IrcDaemon.Replies.SendPasswordMismatch(info);
        }
        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}