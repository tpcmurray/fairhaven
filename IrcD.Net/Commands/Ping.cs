using System.Collections.Generic;
using System.Linq;

namespace IrcD.Commands
{
    public class PingArgument : CommandArgument
    {
        public PingArgument(InfoBase receiver)
            : base(null, receiver, "PING")
        {
        }
    }

    public class Ping : CommandBase
    {
        public Ping(IrcDaemon ircDaemon)
            : base(ircDaemon, "PING", "G")
        { }

        [CheckRegistered]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            Send(new PongArgument(info, args.FirstOrDefault()));
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            var arg = GetSaveArgument<PingArgument>(commandArgument);

            Command.Length = 0;
            Command.Append("PING ");
            Command.Append(IrcDaemon.ServerPrefix);
            return arg.Receiver.WriteLine(Command);
        }
    }
}