using System.Collections.Generic;
using IrcD.Commands;

namespace IrcD.Modes.UserModes
{
    class ModeOperator : UserMode
    {
        public ModeOperator() : base('o') { }
    }
}