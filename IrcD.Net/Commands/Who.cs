using System;
using System.Collections.Generic;
using System.Linq;
using IrcD.Channel;
using IrcD.Modes.UserModes;
using IrcD.Utils;

namespace IrcD.Commands
{
    public class Who : CommandBase
    {
        public Who(IrcDaemon ircDaemon)
            : base(ircDaemon, "WHO", "H")
        { }

        [CheckRegistered]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            IEnumerable<UserPerChannelInfo> whoList;
            var filterInvisible = true;
            if(!info.PassAccepted)
            {
                IrcDaemon.Replies.SendPasswordMismatch(info);
                return;
            }
            ChannelInfo channel;
            var mask = string.Empty;
            if(args.Count < 1 || args[0] == "0")
            {
                whoList = IrcDaemon.Nicks.SelectMany(n => n.Value.UserPerChannelInfos);
            }
            else if(args.Count > 0 && IrcDaemon.Channels.TryGetValue(args[0], out channel))
            {
                whoList = channel.UserPerChannelInfos.Values;
                if(!channel.UserPerChannelInfos.ContainsKey(info.Nick))
                {
                    filterInvisible = false;
                }
            }
            else
            {
                mask = args[0];
                var wildCard = new WildCard(mask, WildcardMatch.Anywhere);
                whoList = IrcDaemon.Nicks.Values.Where(u => wildCard.IsMatch(u.Usermask)).SelectMany(n => n.UserPerChannelInfos);
            }
            if(filterInvisible)
            {
                whoList = whoList.Where(w => !w.UserInfo.Modes.Exist<ModeInvisible>());
            }
            if(args.Count > 1 && args[1] == "o")
            {
                whoList = whoList.Where(w => w.UserInfo.Modes.Exist<ModeOperator>() || w.UserInfo.Modes.Exist<ModeLocalOperator>());
            }
            foreach(var who in whoList)
            {
                IrcDaemon.Replies.SendWhoReply(info, who);
            }
            IrcDaemon.Replies.SendEndOfWho(info, mask);
        }
 
        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}