using System.Collections.Generic;
using IrcD.Channel;
using IrcD.Commands;

namespace IrcD.Modes.ChannelModes
{
    public class ModePrivate : ChannelMode
    {
        public ModePrivate()
            : base('p')
        {
        }
 
        public override bool HandleEvent(CommandBase command, ChannelInfo channel, UserInfo user, List<string> args)
        {
            return true;
        }
    }
}