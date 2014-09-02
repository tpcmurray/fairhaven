using System;
using System.Collections.Generic;

namespace IrcD.Commands
{
    public class Capabilities : CommandBase
    {
        public Capabilities(IrcDaemon ircDaemon)
            : base(ircDaemon, "CAP", "")
        { }

        [CheckParamCount(1)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            var subcommand = args[0];
            switch(subcommand)
            {
                case "LS":
                    break;
                case "LIST":
                    break;
                case "REQ":
                    break;
                case "ACK":
                    break;
                case "NAK":
                    break;
                case "CLEAR":
                    break;
                case "END":
                    break;
                default:
                    IrcDaemon.Replies.SendInvalidCapabilitiesCommand(info, subcommand);
                    break;
            }
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }
    }
}