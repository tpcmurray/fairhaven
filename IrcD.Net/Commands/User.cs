using System;
using System.Collections.Generic;
using IrcD.Modes.UserModes;

namespace IrcD.Commands
{
    public class User : CommandBase
    {
        public User(IrcDaemon ircDaemon)
            : base(ircDaemon, "USER", "USER")
        { }

        [CheckParamCount(4)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            if(!info.PassAccepted)
            {
                IrcDaemon.Replies.SendPasswordMismatch(info);
                return;
            }
            if(info.UserExists)
            {
                IrcDaemon.Replies.SendAlreadyRegistered(info);
                return;
            }
            int flags;
            int.TryParse(args[1], out flags);
            if((flags & 8) > 0)
            {
                info.Modes.Add(new ModeInvisible());
            }
            if((flags & 4) > 0)
            {
                info.Modes.Add(new ModeWallops());
            }
            info.InitUser(args[0], args[3]);
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}