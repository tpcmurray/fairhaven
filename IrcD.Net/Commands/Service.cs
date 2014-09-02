using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class Service : CommandBase
    {
        public Service(IrcDaemon ircDaemon)
            : base(ircDaemon, "SERVICE", "")
        { }

        [CheckParamCount(6)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            if(info.Registered)
            {
                IrcDaemon.Replies.SendAlreadyRegistered(info);
                return;
            }
            if(!IrcDaemon.ValidNick(args[0]))
            {
                IrcDaemon.Replies.SendErroneousNickname(info, args[0]);
                return;
            }
            if(IrcDaemon.Nicks.ContainsKey(args[0]))
            {
                IrcDaemon.Replies.SendNicknameInUse(info, args[0]);
                return;
            }
            info.IsService = true;
            info.InitNick(args[0]);
            info.InitUser("service", "I am a service");
            IrcDaemon.Nicks.Add(info.Nick, info);
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}