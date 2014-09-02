using System.Collections.Generic;
using IrcD.Channel;
using IrcD.Commands;

namespace IrcD.Modes.ChannelModes
{
    public class ModeLimit : ChannelMode, IParameterC
    {
        public ModeLimit() :
            base('l')
        {
        }

        private int limit;
        public string Parameter
        {
            get
            {
                return limit.ToString();
            }
            set
            {
                SetLimit(value);
            }
        }

        private void SetLimit(string value)
        {
            int.TryParse(value, out limit);
            if(limit < 1)
            {
                limit = 1;
            }
        }
        
        public override bool HandleEvent(CommandBase command, ChannelInfo channel, UserInfo user, List<string> args)
        {
            if(command is Join)
            {
                if(limit <= channel.UserPerChannelInfos.Count)
                {
                    user.IrcDaemon.Replies.SendChannelIsFull(user, channel);
                    return false;
                }
            }
            return true;
        }

        public string Add(string parameter)
        {
            SetLimit(parameter);
            return Parameter;
        }
    }
}