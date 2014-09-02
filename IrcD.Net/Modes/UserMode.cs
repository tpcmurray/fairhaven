using System.Collections.Generic;
using IrcD.Commands;

namespace IrcD.Modes
{
    public abstract class UserMode : Mode
    {
        protected UserMode(char mode) : base(mode) { }

        public bool HandleEvent(CommandBase command, UserInfo user, List<string> args)
        {
            return true;
        }
    }
}