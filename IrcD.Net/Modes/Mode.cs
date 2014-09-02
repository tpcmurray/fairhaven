
namespace IrcD.Modes
{
    public class Mode
    {
        public Mode(char mode) 
        { 
            Char = mode; 
        }

        public char Char { get; private set; }
    }
}