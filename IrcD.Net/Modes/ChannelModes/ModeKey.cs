using System.Collections.Generic;
using System.Linq;
using IrcD.Channel;
using IrcD.Commands;

namespace IrcD.Modes.ChannelModes
{
    public class ModeKey : ChannelMode, IParameterB
    {
        public ModeKey()
            : base('k')
        {
        }

        public string Parameter { get; set; }

        public override bool HandleEvent(CommandBase command, ChannelInfo channel, UserInfo user, List<string> args)
        {
            if(command is Join)
            {
                var keys = args.Count > 1 ? (IEnumerable<string>)CommandBase.GetSubArgument(args[1]) : new List<string>();
                if(!keys.Any(k => k == Parameter))
                {
                    user.IrcDaemon.Replies.SendBadChannelKey(user, channel);
                    return false;
                }
            }
            return true;
        }
        
        public string Add(string parameter)
        {
            Parameter = parameter;
            return Parameter;
        }
    }
}