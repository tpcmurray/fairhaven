using System.Collections.Generic;
using System.Linq;
using IrcD.Channel;
using IrcD.Commands;

namespace IrcD.Modes.ChannelModes
{
    public class ModeInviteException : ChannelMode, IParameterListA
    {
        public ModeInviteException()
            : base('I')
        {
        }

        public List<string> MyProperty { get; private set; }

        private readonly List<string> inviteExceptionList = new List<string>();
        public List<string> Parameter
        {
            get { return inviteExceptionList; }
        }

        public void SendList(UserInfo info, ChannelInfo chan)
        {
            foreach(var invite in inviteExceptionList)
            {
                info.IrcDaemon.Replies.SendInviteList(info, chan, invite);
            }
            info.IrcDaemon.Replies.SendEndOfInviteList(info, chan);
        }

        public override bool HandleEvent(CommandBase command, ChannelInfo channel, UserInfo user, List<string> args)
        {
            // Handling JOIN is done in the ModeInvite class
            return true;
        }
        
        public string Add(string parameter)
        {
            parameter = UserInfo.NormalizeHostmask(parameter);
            inviteExceptionList.Add(parameter);
            return parameter;
        }

        public string Remove(string parameter)
        {
            parameter = UserInfo.NormalizeHostmask(parameter);
            return inviteExceptionList.RemoveAll(p => p == parameter) > 0 ? parameter : null;
        }
        
        public override IEnumerable<string> Support(IrcDaemon ircDaemon)
        {
            return Enumerable.Repeat("INEVEX=" + Char, 1);
        }
    }
}
