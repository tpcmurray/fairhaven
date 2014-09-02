using System.Collections.Generic;
using IrcD.Channel;
using IrcD.Commands;

namespace IrcD.Modes.ChannelModes
{
    public class ModeModerated : ChannelMode
    {
        public ModeModerated()
            : base('m')
        {
        }

        public override bool HandleEvent(CommandBase command, ChannelInfo channel, UserInfo user, List<string> args)
        {
            if(command is PrivateMessage || command is Notice)
            {
                UserPerChannelInfo upci;
                if(!channel.UserPerChannelInfos.TryGetValue(user.Nick, out upci) || upci.RankList.Level < 10)
                {
                    user.IrcDaemon.Replies.SendCannotSendToChannel(user, channel.Name);
                    return false;
                }
            }
            return true;
        }
    }
}
