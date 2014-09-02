using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class Summon : CommandBase
    {
        public Summon(IrcDaemon ircDaemon)
            : base(ircDaemon, "SUMMON", "")
        { }

        [CheckRegistered]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            IrcDaemon.Replies.SendSummonDisabled(info);
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}