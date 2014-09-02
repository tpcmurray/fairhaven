using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class MessageOfTheDay : CommandBase
    {
        public MessageOfTheDay(IrcDaemon ircDaemon)
            : base(ircDaemon, "MOTD", "MO")
        { }

        [CheckRegistered]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            // TODO: parameter 1 parsing
            if(string.IsNullOrEmpty(IrcDaemon.Options.MessageOfTheDay))
            {
                IrcDaemon.Replies.SendNoMotd(info);
            }
            else
            {
                IrcDaemon.Replies.SendMotdStart(info);
                IrcDaemon.Replies.SendMotd(info);
                IrcDaemon.Replies.SendMotdEnd(info);
            }
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}
