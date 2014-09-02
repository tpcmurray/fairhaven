using System;
using System.Collections.Generic;
using IrcD.Modes.UserModes;

namespace IrcD.Commands
{
    public class Restart : CommandBase
    {
        public Restart(IrcDaemon ircDaemon)
            : base(ircDaemon, "RESTART", "RESTART")
        { }

        [CheckRegistered]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            if(!info.Modes.Exist<ModeOperator>() && !info.Modes.Exist<ModeLocalOperator>())
            {
                IrcDaemon.Replies.SendNoPrivileges(info);
                return;
            }
            IrcDaemon.Stop(true);
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}