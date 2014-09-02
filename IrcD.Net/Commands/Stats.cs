using System;
using System.Collections.Generic;
using System.Linq;
using IrcD.Modes.UserModes;

namespace IrcD.Commands
{
    public class Stats : CommandBase
    {
        public Stats(IrcDaemon ircDaemon)
            : base(ircDaemon, "STATS", "R")
        { }

        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            if(args.Count < 1)
            {
                IrcDaemon.Replies.SendEndOfStats(info, "-");
            }
            //ToDo: parse target parameter
            if(args[0].Any(l => l == 'l'))
            {
                IrcDaemon.Replies.SendStatsLinkInfo(info);
            }
            if(args[0].Any(l => l == 'm'))
            {
                foreach(var command in IrcDaemon.Commands.OrderBy(c => c.Name))
                {
                    IrcDaemon.Replies.SendStatsCommands(info, command);
                }
            }
            if(args[0].Any(l => l == 'o'))
            {
                foreach(var op in IrcDaemon.Nicks
                .Select(u => u.Value)
                .Where(n => n.Modes.Exist<ModeOperator>() || n.Modes.Exist<ModeLocalOperator>())
                .OrderBy(o => o.Nick))
                {
                    IrcDaemon.Replies.SendStatsOLine(info, op);
                }
            }
            if(args[0].Any(l => l == 'u'))
            {
                IrcDaemon.Replies.SendStatsUptime(info);
            }
            IrcDaemon.Replies.SendEndOfStats(info, args[0]);
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}