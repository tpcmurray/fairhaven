using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class Time : CommandBase
    {
        public Time(IrcDaemon ircDaemon)
            : base(ircDaemon, "TIME", "TI")
        { }
        
        [CheckRegistered]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            //ToDo: parse target parameter
            IrcDaemon.Replies.SendTimeReply(info);
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}