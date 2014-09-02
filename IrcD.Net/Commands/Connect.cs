using System;
using System.Collections.Generic;
using IrcD.Modes.UserModes;

namespace IrcD.Commands
{
    public class Connect : CommandBase
    {
        public Connect(IrcDaemon ircDaemon)
            : base(ircDaemon, "CONNECT", "CO")
        { }

        [CheckRegistered]
        [CheckParamCount(1)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            if(!info.Modes.Exist<ModeOperator>() && !info.Modes.Exist<ModeLocalOperator>())
            {
                IrcDaemon.Replies.SendNoPrivileges(info);
                return;
            }
            int port;
            if(int.TryParse(args[1], out port))
            {
                IrcDaemon.Connect(args[0], port);
            }

            IrcDaemon.Replies.SendNoSuchServer(info, "Connect failed");
        }
 
        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}