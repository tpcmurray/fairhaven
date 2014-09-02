using System.Collections.Generic;
using IrcD.Commands;

namespace IrcD.Modes.UserModes
{
    class ModeLocalOperator : UserMode
    {
        public ModeLocalOperator() : base('O') { }
    }
}