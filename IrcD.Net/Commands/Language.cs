using System;
using System.Collections.Generic;
using System.Linq;

namespace IrcD.Commands
{
    class Language : CommandBase
    {
        public Language(IrcDaemon ircDaemon)
            : base(ircDaemon, "LANGUAGE", "")
        { }

        [CheckRegistered]
        [CheckParamCount(1)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            info.Languages = args[0].Split(new[] { ',' });
            IrcDaemon.Replies.SendYourLanguageIs(info);
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> Support(IrcDaemon ircDaemon)
        {
            return Enumerable.Repeat("LANGUAGE=" + ircDaemon.Options.MaxLanguages, 1);
        }
    }
}