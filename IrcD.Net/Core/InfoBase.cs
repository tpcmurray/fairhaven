using System.Text;

namespace IrcD
{
    public abstract class InfoBase
    {
        protected InfoBase(IrcDaemon ircDaemon)
        {
            IrcDaemon = ircDaemon;
        }

        public IrcDaemon IrcDaemon { get; private set; }

        /// <summary>
        /// Write a Line to the abstract object(hide the socket better)
        /// </summary>
        public abstract int WriteLine(StringBuilder line);
        public abstract int WriteLine(StringBuilder line, UserInfo exception);
    }
}