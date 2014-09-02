using System.Collections.Generic;
using System.Linq;

namespace IrcD.Commands
{
    public class QuitArgument : CommandArgument
    {
        public QuitArgument(UserInfo sender, InfoBase receiver, string message)
            : base(sender, receiver, "QUIT")
        {
            Message = message;
        }
        public string Message { get; private set; }
    }

    public class Quit : CommandBase
    {
        public Quit(IrcDaemon ircDaemon)
            : base(ircDaemon, "QUIT", "Q")
        { }
    
        [CheckRegistered]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            var message = (args.Count > 0) ? args.First() : IrcDaemon.Options.StandardQuitMessage;
            info.Remove(message);
        }
        
        protected override int PrivateSend(CommandArgument commandArgument)
        {
            var arg = GetSaveArgument<QuitArgument>(commandArgument);
            BuildMessageHeader(arg);
            Command.Append(arg.Message);
            return arg.Receiver.WriteLine(Command);
        }
    }
}