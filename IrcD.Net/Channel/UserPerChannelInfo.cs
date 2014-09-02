using System.Text;
using IrcD.Modes;

namespace IrcD.Channel
{
    public class UserPerChannelInfo:InfoBase
    {
        public UserPerChannelInfo(UserInfo userInfo, ChannelInfo channelInfo)
            : base(userInfo.IrcDaemon)
        {
            UserInfo = userInfo;
            ChannelInfo = channelInfo;
            RankList = new RankList(userInfo.IrcDaemon);
        }

        public UserInfo UserInfo { get; private set; }
        public ChannelInfo ChannelInfo { get; private set; }
        public RankList RankList { get; private set; }

        public override int WriteLine(StringBuilder line)
        {
            return UserInfo.WriteLine(line);
        }

        public override int WriteLine(StringBuilder line, UserInfo exception)
        {
            if(UserInfo != exception)
            {
                return UserInfo.WriteLine(line);
            }
            return 0;
        }
    }
}