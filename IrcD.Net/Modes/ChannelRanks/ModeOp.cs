namespace IrcD.Modes.ChannelRanks
{
    class ModeOp : ChannelRank
    {
        public ModeOp() : base('o', '@', 50) { }

        public override bool CanChangeChannelMode(ChannelMode mode)
        {
            return true;
        }

        public override bool CanChangeChannelRank(ChannelRank rank)
        {
            return true;
        }

    }
}