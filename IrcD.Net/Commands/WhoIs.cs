using System;
using System.Collections.Generic;
using IrcD.Modes.UserModes;

namespace IrcD.Commands
{
    public class WhoIs : CommandBase
    {
        public WhoIs(IrcDaemon ircDaemon)
            : base(ircDaemon, "WHOIS", "W")
        { }
        [CheckRegistered]
        [CheckParamCount(1)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            if(!IrcDaemon.Nicks.ContainsKey(args[0]))
            {
                IrcDaemon.Replies.SendNoSuchNick(info, args[0]);
                return;
            }
            var user = IrcDaemon.Nicks[args[0]];
            IrcDaemon.Replies.SendWhoIsUser(info, user);
            if(info.UserPerChannelInfos.Count > 0)
            {
                IrcDaemon.Replies.SendWhoIsChannels(info, user);
            }
            IrcDaemon.Replies.SendWhoIsServer(info, user);
            if(user.AwayMessage != null)
            {
                IrcDaemon.Replies.SendAwayMessage(info, user);
            }
            if(IrcDaemon.Options.IrcMode == IrcMode.Modern)
            {
                IrcDaemon.Replies.SendWhoIsLanguage(info, user);
            }

            if(user.Modes.Exist<ModeOperator>() || user.Modes.Exist<ModeLocalOperator>())
            {
                IrcDaemon.Replies.SendWhoIsOperator(info, user);
            }
            IrcDaemon.Replies.SendWhoIsIdle(info, user);
            IrcDaemon.Replies.SendEndOfWhoIs(info, user);
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}