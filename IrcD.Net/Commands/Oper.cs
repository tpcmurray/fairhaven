using System;
using System.Collections.Generic;
using IrcD.Modes.UserModes;

namespace IrcD.Commands
{
    public class Oper : CommandBase
    {
        public Oper(IrcDaemon ircDaemon)
            : base(ircDaemon, "OPER", "OPER")
        { }

        [CheckRegistered]
        [CheckParamCount(2)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            if(DenyOper(info))
            {
                IrcDaemon.Replies.SendNoOperHost(info);
                return;
            }
            if(ValidOperLine(args[0], args[1]))
            {
                info.Modes.Add(new ModeLocalOperator());
                info.Modes.Add(new ModeOperator());
                IrcDaemon.Replies.SendYouAreOper(info);
            }
            else
            {
                IrcDaemon.Replies.SendPasswordMismatch(info);
            }
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check if an IRC Operator status can be granted upon user and pass
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public bool ValidOperLine(string user, string pass)
        {
            string realpass;
            if(IrcDaemon.Options.OLine.TryGetValue(user, out realpass))
            {
                return pass == realpass;
            }
            return false;
        }
 
        /// <summary>
        /// Check if the Host of the user is allowed to use the OPER command
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private bool DenyOper(UserInfo info)
        {
            var allow = false;
            foreach(var operHost in IrcDaemon.Options.OperHosts)
            {
                if(!allow && operHost.Allow)
                    allow = operHost.WildcardHostMask.IsMatch(info.Host);
                if(allow && !operHost.Allow)
                    allow = !operHost.WildcardHostMask.IsMatch(info.Host);
            }
            return !allow;
        }
    }
}