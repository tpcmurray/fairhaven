using System.Collections.Generic;

namespace IrcD.Modes
{
    public class ModeFactory
    {
        public T GetConstructor<T>() where T : Mode, new()
        {
            return new T();
        }

        public delegate ChannelMode ConstructChannelMode();
        public delegate ChannelRank ConstructChannelRank();
        public delegate UserMode ConstructUserMode();

        private readonly Dictionary<char, ConstructChannelMode> channelModeFactory = new Dictionary<char, ConstructChannelMode>();
        public T AddChannelMode<T>() where T : ChannelMode, new()
        {
            var mode = GetConstructor<T>();
            channelModeFactory.Add(mode.Char, GetConstructor<T>);
            return mode;
        }

        public ChannelMode GetChannelMode(char c)
        {
            ConstructChannelMode channelMode;
            return channelModeFactory.TryGetValue(c, out channelMode) ? channelMode.Invoke() : null;
        }


        private readonly Dictionary<char, ConstructChannelRank> channelRankFactory = new Dictionary<char, ConstructChannelRank>();
        public T AddChannelRank<T>() where T : ChannelRank, new()
        {
            var mode = GetConstructor<T>();
            channelRankFactory.Add(mode.Char, GetConstructor<T>);
            return mode;
        }

        public ChannelRank GetChannelRank(char c)
        {
            ConstructChannelRank channelRank;
            return channelRankFactory.TryGetValue(c, out channelRank) ? channelRank.Invoke() : null;
        }


        private readonly Dictionary<char, ConstructUserMode> userModeFactory = new Dictionary<char, ConstructUserMode>();
        public T AddUserMode<T>() where T : UserMode, new()
        {
            var mode = GetConstructor<T>();
            userModeFactory.Add(mode.Char, GetConstructor<T>);
            return mode;
        }

        public UserMode GetUserMode(char c)
        {
            ConstructUserMode userMode;
            return userModeFactory.TryGetValue(c, out userMode) ? userMode.Invoke() : null;
        }
    }
}
