﻿using System;
using System.Linq;
using IrcD.Modes.UserModes;

namespace IrcD
{
    public class ServerStats
    {
        readonly IrcDaemon ircDaemon;
        public ServerStats(IrcDaemon ircDaemon)
        {
            this.ircDaemon = ircDaemon;
        }

        public int ServerCount { get { return 1; } }
        public int ServiceCount { get { return ircDaemon.Sockets.Count(s => s.Value.IsService); } }
        public int UserCount { get { return ircDaemon.Nicks.Count - ServiceCount; } }
        public int OperatorCount { get { return ircDaemon.Sockets.Count(s => s.Value.Modes.Exist<ModeOperator>()); } }
        public int LocalOperatorCount { get { return ircDaemon.Sockets.Count(s => s.Value.Modes.Exist<ModeLocalOperator>()); } }
        public int UnknowConnectionCount { get { return ircDaemon.Sockets.Count(s => !s.Value.IsAcceptSocket); } }
        public int ChannelCount { get { return ircDaemon.Channels.Count; } }
        public int ClientCount { get { return ircDaemon.Nicks.Count; } }
        public TimeSpan Uptime { get { return DateTime.Now - ircDaemon.ServerCreated; } }
    }
}