namespace IrcD.Modes.ChannelRanks
{
    class ModeNoRank : ChannelRank
    {
        private ModeNoRank() : base(' ', ' ', 0) { }

        private static ModeNoRank instance;
        public static ModeNoRank Instance
        {
            get
            {
                return instance ?? (instance = new ModeNoRank());
            }
        }
    }
}
