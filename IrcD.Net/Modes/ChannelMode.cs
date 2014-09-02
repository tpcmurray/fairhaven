using System.Collections.Generic;
using System.Linq;
using IrcD.Channel;
using IrcD.Commands;

namespace IrcD.Modes
{
    public abstract class ChannelMode : Mode
    {
        protected ChannelMode(char mode) : base(mode) { }

        /// <summary>
        /// This method is called to check all the modes on a channel, each Mode has the chance to take control 
        /// over a command. If it takes control it should return false, therefore the other Commands are not 
        /// checked, and the control flow will interrupt.
        /// </summary>
        /// <param name="command">Type of command</param>
        /// <param name="channel">The Channel the Mode is operating</param>
        /// <param name="user">The User which uses the Command on the channel</param>
        /// <returns>Handle Event should return true when the command is allowed to proceed normally. 
        /// It should return false, if the Mode forbids the further execution of the Command.</returns>
        public abstract bool HandleEvent(CommandBase command, ChannelInfo channel, UserInfo user, List<string> args);

        /// <summary>
        /// Returns a list of ISUPPORT / Numeric 005 information this Mode is providing. 
        /// </summary>
        /// <param name="ircDaemon">Server Object</param>
        /// <returns>returns strings for direct usage in an ISUPPORT reply</returns>
        public virtual IEnumerable<string> Support(IrcDaemon ircDaemon)
        {
            return Enumerable.Empty<string>();
        }
    }
}