namespace IrcD.Channel
{
    public class ChannelType
    {
        public ChannelType(char prefix, int maxJoinedAllowed)
        {
            Prefix = prefix;
            MaxJoinedAllowed = maxJoinedAllowed;
        }

        public char Prefix { get; private set; }
        public int MaxJoinedAllowed { get; set; }
    }
}