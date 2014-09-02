using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class UserHost : CommandBase
    {
        public UserHost(IrcDaemon ircDaemon)
            : base(ircDaemon, "USERHOST", "USERHOST")
        { }

        [CheckRegistered]
        [CheckParamCount(1)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            var users = new List<UserInfo>();
            foreach(var arg in args)
            {
                UserInfo user;
                if(IrcDaemon.Nicks.TryGetValue(arg, out user))
                {
                    users.Add(user);
                }
            }
            IrcDaemon.Replies.SendUserHost(info, users);
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}