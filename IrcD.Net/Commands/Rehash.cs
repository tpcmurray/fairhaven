using System;
using System.Collections.Generic;
using IrcD.Modes.UserModes;

namespace IrcD.Commands
{
    public class Rehash : CommandBase
    {
        public Rehash(IrcDaemon ircDaemon)
            : base(ircDaemon, "REHASH", "REHASH")
        { }

        [CheckRegistered]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            if(info.Modes.Exist<ModeOperator>() || info.Modes.Exist<ModeLocalOperator>())
            {
                IrcDaemon.OnRehashEvent(this, new RehashEventArgs(IrcDaemon, info));
            }
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}