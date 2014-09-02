using IrcD.ServerReplies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrcD.Commands
{
    public class Begin : CommandBase
    {
        public Begin(IrcDaemon ircDaemon)
            : base(ircDaemon, "BEGIN", "BG")
        { }
        
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            //IrcDaemon.Replies.SendFairhaven(info, (ReplyCode)256, "Response to BEGIN command.");
            Send(new PrivateMessageArgument(IrcDaemon._q, info, info.Nick, "response to BEGIN command"));
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}
