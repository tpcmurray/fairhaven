using System.Collections.Generic;
using IrcD.Modes.UserModes;

namespace IrcD.Commands
{
    public class KillArgument : CommandArgument
    {
        public KillArgument(UserInfo sender, UserInfo receiver, string message)
            : base(sender, receiver, "KILL")
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
    
    public class Kill : CommandBase
    {
        public Kill(IrcDaemon ircDaemon)
            : base(ircDaemon, "KILL", "D")
        { }

        [CheckRegistered]
        [CheckParamCount(1)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            if(!info.Modes.Exist<ModeOperator>() && !info.Modes.Exist<ModeLocalOperator>())
            {
                IrcDaemon.Replies.SendNoPrivileges(info);
                return;
            }
            UserInfo killUser;
            if(!IrcDaemon.Nicks.TryGetValue(args[0], out killUser))
            {
                IrcDaemon.Replies.SendNoSuchNick(info, args[0]);
            }
            var message = (args.Count > 1) ? args[1] : IrcDaemon.Options.StandardKillMessage;
            Send(new KillArgument(info, killUser, message));
            killUser.Remove(IrcDaemon.Options.StandardKillMessage);
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            var arg = GetSaveArgument<KillArgument>(commandArgument);
            BuildMessageHeader(arg);
            var user = arg.Receiver as UserInfo;
            Command.Append((user != null) ? user.Nick : "nobody");
            Command.Append(" :");
            Command.Append(arg.Message);
            return arg.Receiver.WriteLine(Command);
        }
    }
}
