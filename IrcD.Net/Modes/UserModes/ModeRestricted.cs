using System.Collections.Generic;
using IrcD.Commands;

namespace IrcD.Modes.UserModes
{
    public class ModeRestricted : UserMode
    {
        public ModeRestricted() : base('r') { }
    }
}