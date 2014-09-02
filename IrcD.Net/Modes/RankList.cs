using System.Linq;
using System.Text;
using IrcD.Modes.ChannelRanks;

namespace IrcD.Modes
{
    public class RankList : ModeList<ChannelRank>
    {
        public RankList(IrcDaemon ircDaemon)
            : base(ircDaemon)
        {
        }

        public string ToPrefixList()
        {
            var ranks = new StringBuilder();
            ranks.Append("(");
            foreach(var rank in Values.OrderByDescending(rank => rank.Level))
            {
                ranks.Append(rank.Char);
            }
            ranks.Append(")");
            foreach(var rank in Values.OrderByDescending(rank => rank.Level))
            {
                ranks.Append(rank.Prefix);
            }
            return ranks.ToString();
        }

        public char NickPrefixRaw
        {
            get
            {
                return this
                .OrderByDescending(rank => rank.Value.Level)
                .Select(rank => rank.Value.Prefix)
                .DefaultIfEmpty(' ')
                .First();
            }
        }

        public string NickPrefix
        {
            get
            {
                return NickPrefixRaw != ' ' ? NickPrefixRaw.ToString() : string.Empty;
            }
        }

        public ChannelRank CurrentRank
        {
            get
            {
                return this
                .OrderByDescending(rank => rank.Value.Level)
                .Select(rank => rank.Value)
                .DefaultIfEmpty(ModeNoRank.Instance)
                .First();
            }
        }

        public int Level
        {
            get
            {
                return this
                .OrderByDescending(rank => rank.Value.Level)
                .Select(rank => rank.Value.Level)
                .DefaultIfEmpty(0)
                .First();
            }
        }
    }
}