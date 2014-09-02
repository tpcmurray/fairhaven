using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrcD.Modes
{
    public class ModeList<TMode> : SortedList<char, TMode> where TMode : Mode
    {
        protected IrcDaemon IrcDaemon;
        public ModeList(IrcDaemon ircDaemon)
        {
            IrcDaemon = ircDaemon;
        }

        public void Add<T>(T element) where T : TMode
        {
            if(!Exist<T>())
            {
                Add(element.Char, element);
            }
        }

        public T GetMode<T>() where T : TMode
        {
            return Values.Where(mode => mode is T).FirstOrDefault() as T;
        }

        public void RemoveMode<T>() where T : TMode
        {
            if(Exist<T>())
            {
                var mode = GetMode<T>();
                Remove(mode.Char);
            }
        }

        public override string ToString()
        {
            var modes = new StringBuilder();
            foreach(var mode in Values)
            {
                modes.Append(mode.Char);
            }
            return modes.ToString();
        }

        public bool Exist<TExist>() where TExist : TMode
        {
            return Values.Any(m => m is TExist);
        }
    }
}