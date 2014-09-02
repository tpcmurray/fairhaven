using System.Collections.Generic;
using System.Linq;
using System.Text;
using IrcD.Modes;

namespace IrcD.Channel
{
    public class ChannelInfo:InfoBase
    {
        public ChannelInfo(string name, IrcDaemon ircDaemon)
            : base(ircDaemon)
        {
            Name = name;
            Modes = new ChannelModeList(ircDaemon);
            ChannelType = ircDaemon.SupportedChannelTypes[name[0]];
            UserPerChannelInfos = new Dictionary<string, UserPerChannelInfo>();
        }

        public string Name { get; internal set; }
        public string Topic { get; set; }
        public string ModeString { get { return Modes.ToChannelModeString(); } }
        public ChannelModeList Modes { get; internal set; }
        public ChannelType ChannelType { get; internal set; }
        public Dictionary<string, UserPerChannelInfo> UserPerChannelInfos { get; private set; }

        public IEnumerable<UserInfo> Users
        {
            get
            {
                return UserPerChannelInfos.Select(upci => upci.Value.UserInfo);
            }
        }

        public char NamesPrefix
        {
            get
            {
                return Modes.IsPrivate() ? '*' : (Modes.IsSecret() ? '@' : '=');
            }
        }
        public override int WriteLine(StringBuilder line)
        {
            return WriteLine(line, null);
        }

        public override int WriteLine(StringBuilder line, UserInfo exception)
        {
            int bytes = 0;
            foreach(var user in Users)
            {
                bytes += user.WriteLine(line, exception);
            }
            return bytes;
        }
    }
}