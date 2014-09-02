using System.Collections.Generic;
using System.Linq;
using IrcD.Channel;
using IrcD.Commands;
using IrcD.Utils;

namespace IrcD.Modes.ChannelModes
{
    public class ModeBan : ChannelMode, IParameterListA
    {
        public ModeBan() : base('b') { }
        private readonly List<string> banList = new List<string>();
        public List<string> Parameter
        {
            get { return banList; }
        }

        public void SendList(UserInfo info, ChannelInfo chan)
        {
            foreach(var ban in banList)
            {
                info.IrcDaemon.Replies.SendBanList(info, chan, ban);
            }
            info.IrcDaemon.Replies.SendEndOfBanList(info, chan);
        }

        public override bool HandleEvent(CommandBase command, ChannelInfo channel, UserInfo user, List<string> args)
        {
            if(command is Join)
            {
                if(banList.Select(ban => new WildCard(ban, WildcardMatch.Exact)).Any(usermask => usermask.IsMatch(user.Usermask)))
                {
                    user.IrcDaemon.Replies.SendBannedFromChannel(user, channel);
                    return false;
                }
            }
            if(command is PrivateMessage || command is Notice)
            {
                if(banList.Select(ban => new WildCard(ban, WildcardMatch.Exact)).Any(usermask => usermask.IsMatch(user.Usermask)))
                {
                    user.IrcDaemon.Replies.SendCannotSendToChannel(user, channel.Name, "You are banned from the Channel");
                    return false;
                }
            }
            return true;
        }

        public string Add(string parameter)
        {
            parameter = UserInfo.NormalizeHostmask(parameter);
            banList.Add(parameter);
            return parameter;
        }

        public string Remove(string parameter)
        {
            parameter = UserInfo.NormalizeHostmask(parameter);
            return banList.RemoveAll(p => p == parameter) > 0 ? parameter : null;
        }
    }
}