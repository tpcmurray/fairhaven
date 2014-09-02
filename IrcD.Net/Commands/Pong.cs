using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class PongArgument : CommandArgument
    {
        public PongArgument(InfoBase receiver, string parameter)
            : base(null, receiver, "PONG")
        {
            Parameter = parameter;
        }

        public string Parameter { get; private set; }
    }
    
    public class Pong : CommandBase
    {
        public Pong(IrcDaemon ircDaemon)
            : base(ircDaemon, "PONG", "Z")
        { }

        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            var arg = GetSaveArgument<PongArgument>(commandArgument);
            Command.Length = 0;
            Command.Append(IrcDaemon.ServerPrefix);
            Command.Append(" ");
            Command.Append(arg.Name);
            Command.Append(" ");
            Command.Append(IrcDaemon.ServerPrefix);
            Command.Append(" ");
            Command.Append(arg.Parameter);
            return arg.Receiver.WriteLine(Command);
        }
    }
}