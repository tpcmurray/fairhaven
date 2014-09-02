using System.Collections.Generic;
using IrcD.Channel;
using IrcD.Commands;

namespace IrcD.Modes.ChannelModes
{
    public class ModeTopic : ChannelMode
    {
        public ModeTopic()
            : base('t')
        {
        }

        public override bool HandleEvent(CommandBase command, ChannelInfo channel, UserInfo user, List<string> args)
        {
            if(command is Topic)
            {
                UserPerChannelInfo upci;
                if(!channel.UserPerChannelInfos.TryGetValue(user.Nick, out upci))
                {
                    user.IrcDaemon.Replies.SendNotOnChannel(user, channel.Name);
                    return false;
                }
                if(upci.RankList.Level < 30)
                {
                    user.IrcDaemon.Replies.SendChannelOpPrivilegesNeeded(user, channel);
                    return false;
                }
            }
            return true;
        }
    }
}