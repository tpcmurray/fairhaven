using System.Collections.Generic;
using System.Linq;
using System.Text;
using IrcD.Commands;

namespace IrcD.Modes
{
    public class UserModeList : ModeList<UserMode>
    {
        public UserModeList(IrcDaemon ircDaemon)
            : base(ircDaemon)
        {
        }

        public bool HandleEvent(CommandBase command, UserInfo user, List<string> args)
        {
            return Values.All(mode => mode.HandleEvent(command, user, args));
        }

        internal void Update(UserInfo info, IEnumerable<string> args)
        {
            var plus = true;
            var lastprefix = ' ';
            var validmode = new StringBuilder();
            foreach(var modechar in args.First())
            {
                if(modechar == '+' || modechar == '-')
                {
                    plus = (modechar == '+');
                    continue;
                }
                var umode = IrcDaemon.ModeFactory.GetUserMode(modechar);
                if(umode == null) continue;
                if(plus)
                {
                    if(!ContainsKey(umode.Char))
                    {
                        Add(umode);
                        if(lastprefix != '+')
                        {
                            validmode.Append(lastprefix = '+');
                        }
                        validmode.Append(umode.Char);
                    }
                }
                else
                {
                    if(ContainsKey(umode.Char))
                    {
                        Remove(umode.Char);
                        if(lastprefix != '-')
                        {
                            validmode.Append(lastprefix = '-');
                        }
                        validmode.Append(umode.Char);
                    }
                }
            }
            info.IrcDaemon.Commands.Send(new ModeArgument(info, info, info.Nick, validmode.ToString()));
        }

        public string ToUserModeString()
        {
            return "+" + ToString();
        }
    }
}