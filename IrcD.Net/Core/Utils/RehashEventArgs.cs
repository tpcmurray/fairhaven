using System;

namespace IrcD
{
    public class RehashEventArgs : EventArgs
    {
        public RehashEventArgs(IrcDaemon ircDaemon, UserInfo userInfo)
        {
            IrcDaemon = ircDaemon;
            UserInfo = userInfo;
        }
        public IrcDaemon IrcDaemon { get; private set; }
        public UserInfo UserInfo { get; private set; }
    }
}