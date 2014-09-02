using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class Info : CommandBase
    {
        public Info(IrcDaemon ircDaemon)
            : base(ircDaemon, "INFO", "F")
        { }

        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            //ToDo: parse target parameter
            IrcDaemon.Replies.SendInfo(info);
            IrcDaemon.Replies.SendEndOfInfo(info);
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}
