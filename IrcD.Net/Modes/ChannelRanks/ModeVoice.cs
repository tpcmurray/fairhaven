namespace IrcD.Modes.ChannelRanks
{
    public class ModeVoice : ChannelRank
    {
        public ModeVoice() : base('v', '+', 10) { }

        public override bool CanSpeakModerated()
        {
            return true;
        }
    }
}