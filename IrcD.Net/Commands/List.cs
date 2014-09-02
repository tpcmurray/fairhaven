using System;
using System.Collections.Generic;
using System.Linq;

namespace IrcD.Commands
{
    public class List : CommandBase
    {
        public List(IrcDaemon ircDaemon)
            : base(ircDaemon, "LIST", "LIST")
        { }

        [CheckRegistered]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            if(info.IrcDaemon.Options.IrcMode == IrcMode.Rfc1459)
                IrcDaemon.Replies.SendListStart(info);
            // TODO: special List commands(if RfcModern)
            foreach(var ci in IrcDaemon.Channels.Values.Where(ci => !ci.Modes.IsPrivate() && !ci.Modes.IsSecret()))
            {
                IrcDaemon.Replies.SendListItem(info, ci);
            }
            IrcDaemon.Replies.SendListEnd(info);
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}