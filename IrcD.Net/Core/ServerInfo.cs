using System;
using System.Text;

namespace IrcD
{
    class ServerInfo : InfoBase
    {
        public ServerInfo(IrcDaemon ircDaemon)
            : base(ircDaemon)
        {
        }

        public override int WriteLine(StringBuilder line)
        {
            throw new NotImplementedException();
        }

        public override int WriteLine(StringBuilder line, UserInfo exception)
        {
            throw new NotImplementedException();
        }
    }
}