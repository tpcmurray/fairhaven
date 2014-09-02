namespace IrcD.Modes.ChannelRanks
{
    class ModeHalfOp : ChannelRank
    {
        public ModeHalfOp() : base('h', '%', 30) { }

        public override bool CanChangeChannelRank(ChannelRank rank)
        {
            return rank.Level <= Level;
        }
    }
}