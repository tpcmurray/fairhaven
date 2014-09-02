
namespace IrcD.Modes
{
    public abstract class ChannelRank : Mode
    {
        protected ChannelRank(char mode, char prefix, int level)
            : base(mode)
        {
            Prefix = prefix;
            Level = level;
        }

        public char Prefix { get; private set; }
        public int Level { get; private set; }

        public virtual bool CanChangeChannelMode(ChannelMode mode)
        {
            return false;
        }

        public virtual bool CanChangeChannelRank(ChannelRank rank)
        {
            return false;
        }

        public virtual bool CanInviteUser(UserInfo user)
        {
            return false;
        }

        public virtual bool CanKickUser(UserInfo user)
        {
            return false;
        }

        public virtual bool CanChangeTopic()
        {
            return false;
        }

        public virtual bool CanSpeakModerated()
        {
            return false;
        }
    }
}