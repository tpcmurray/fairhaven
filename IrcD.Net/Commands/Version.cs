using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class Version : CommandBase
    {
        public Version(IrcDaemon ircDaemon)
            : base(ircDaemon, "VERSION", "V")
        { }

        [CheckRegistered]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            //ToDo: parse target parameter
            IrcDaemon.Replies.SendVersion(info);
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}