using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class Admin : CommandBase
    {
        public Admin(IrcDaemon ircDaemon)
            : base(ircDaemon, "ADMIN", "AD")
        { }
        [CheckRegistered]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            //ToDo: parse target parameter
            IrcDaemon.Replies.SendAdminMe(info);
            IrcDaemon.Replies.SendAdminLocation1(info);
            IrcDaemon.Replies.SendAdminLocation2(info);
            IrcDaemon.Replies.SendAdminEmail(info);
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}