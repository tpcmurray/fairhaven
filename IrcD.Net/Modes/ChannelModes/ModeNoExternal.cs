using System.Collections.Generic;
using IrcD.Channel;
using IrcD.Commands;

namespace IrcD.Modes.ChannelModes
{
    public class ModeNoExternal : ChannelMode
    {
        public ModeNoExternal()
            : base('n')
        {
        }

        public override bool HandleEvent(CommandBase command, ChannelInfo channel, UserInfo user, List<string> args)
        {
            if(command is PrivateMessage || command is Notice)
            {
                if(!channel.UserPerChannelInfos.ContainsKey(user.Nick))
                {
                    user.IrcDaemon.Replies.SendCannotSendToChannel(user, channel.Name);
                    return false;
                }
            }
            return true;
        }
    }
}