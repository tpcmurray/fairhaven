using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class ListUsers : CommandBase
    {
        public ListUsers(IrcDaemon ircDaemon)
            : base(ircDaemon, "LUSERS", "LU")
        { }

        [CheckRegistered]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            IrcDaemon.Replies.SendListUserClient(info);
            IrcDaemon.Replies.SendListUserOp(info);
            IrcDaemon.Replies.SendListUserUnknown(info);
            IrcDaemon.Replies.SendListUserChannels(info);
            IrcDaemon.Replies.SendListUserMe(info);
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}