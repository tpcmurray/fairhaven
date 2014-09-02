using System.Collections.Generic;
using IrcD.Channel;
using IrcD.ServerReplies;
using IrcD.Commands;

namespace IrcD.Modes.ChannelModes
{
    public class ModeInvite : ChannelMode
    {
        public ModeInvite()
            : base('i')
        {
        }

        public override bool HandleEvent(CommandBase command, ChannelInfo channel, UserInfo user, List<string> args)
        {
            if(command is Join)
            {
                if(!user.Invited.Contains(channel))
                {
                    user.IrcDaemon.Replies.SendInviteOnlyChannel(user, channel);
                    return false;
                }
                user.Invited.Remove(channel);
            }
            if(command is Invite)
            {
                UserPerChannelInfo upci;
                if(channel.UserPerChannelInfos.TryGetValue(user.Nick, out upci))
                {
                    if(upci.RankList.Level < 30)
                    {
                        channel.IrcDaemon.Replies.SendChannelOpPrivilegesNeeded(user, channel);
                        return false;
                    }
                }
                else
                {
                    channel.IrcDaemon.Replies.SendNotOnChannel(user, channel.Name);
                    return false;
                }
            }
            return true;
        }
    }
}