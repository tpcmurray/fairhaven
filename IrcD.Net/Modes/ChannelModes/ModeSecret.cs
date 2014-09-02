using System.Collections.Generic;
using IrcD.Channel;
using IrcD.Commands;

namespace IrcD.Modes.ChannelModes
{
    public class ModeSecret : ChannelMode
    {
        public ModeSecret()
            : base('s')
        {
        }

        public override bool HandleEvent(CommandBase command, ChannelInfo channel, UserInfo user, List<string> args)
        {
            if(command is List)
            {
                // TODO
            }
            return true;
        }
    }
}