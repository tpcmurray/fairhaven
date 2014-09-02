using System;
using System.Collections.Generic;
using System.Linq;

namespace IrcD.Commands
{
    public class Silence : CommandBase
    {
        public Silence(IrcDaemon ircDaemon)
            : base(ircDaemon, "SILENCE", "U")
        { }

        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> Support(IrcDaemon ircDaemon)
        {
            return Enumerable.Repeat("SILENCE=" + ircDaemon.Options.MaxSilence, 1);
        }
    }
}