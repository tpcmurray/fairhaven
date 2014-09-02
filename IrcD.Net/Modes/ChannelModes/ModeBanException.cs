using System.Collections.Generic;
using System.Linq;
using IrcD.Channel;
using IrcD.Commands;

namespace IrcD.Modes.ChannelModes
{
    public class ModeBanException : ChannelMode, IParameterListA
    {
        public ModeBanException()
            : base('e')
        {
        }

        private readonly List<string> banExceptionList = new List<string>();
        public List<string> Parameter
        {
            get { return banExceptionList; }
        }

        public void SendList(UserInfo info, ChannelInfo chan)
        {
            foreach(var banExcpetion in banExceptionList)
            {
                info.IrcDaemon.Replies.SendExceptionList(info, chan, banExcpetion);
            }
            info.IrcDaemon.Replies.SendEndOfExceptionList(info, chan);
        }

        public override bool HandleEvent(CommandBase command, ChannelInfo channel, UserInfo user, List<string> args)
        {
            // Handling JOIN is done in the ModeBan class
            return true;
        }

        public string Add(string parameter)
        {
            parameter = UserInfo.NormalizeHostmask(parameter);
            banExceptionList.Add(parameter);
            return parameter;
        }

        public string Remove(string parameter)
        {
            parameter = UserInfo.NormalizeHostmask(parameter);
            return banExceptionList.RemoveAll(p => p == parameter) > 0 ? parameter : null;
        }

        public override IEnumerable<string> Support(IrcDaemon ircDaemon)
        {
            return Enumerable.Repeat("EXCEPTS=" + Char, 1);
        }
    }
}
