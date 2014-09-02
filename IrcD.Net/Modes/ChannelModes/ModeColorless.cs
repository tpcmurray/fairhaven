using System.Collections.Generic;
using System.Linq;
using IrcD.Channel;
using IrcD.Commands;
using IrcD.Utils;

namespace IrcD.Modes.ChannelModes
{
    public class ModeColorless:ChannelMode
    {
        public ModeColorless()
            : base('c')
        {
        }

        public override bool HandleEvent(CommandBase command, ChannelInfo channel, UserInfo user, List<string> args)
        {
            if(command is PrivateMessage || command is Notice)
            {
                if(args[1].Any(c => c == IrcConstants.IrcColor))
                {
                    channel.IrcDaemon.Replies.SendCannotSendToChannel(user, channel.Name, "Color is not permitted in this channel");
                    return false;
                }
                if(args[1].Any(c => c == IrcConstants.IrcBold || c == IrcConstants.IrcNormal || c == IrcConstants.IrcUnderline || c == IrcConstants.IrcReverse))
                {
                    channel.IrcDaemon.Replies.SendCannotSendToChannel(user, channel.Name, "Control codes(bold/underline/reverse) are not permitted in this channel");
                    return false;
                }
            }
            return true;
        }
    }
}