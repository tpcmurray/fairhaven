using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrcD.Commands
{
    public class CommandArgument
    {
        public CommandArgument(UserInfo sender, InfoBase receiver, string name)
        {
            Name = name;
            Sender = sender;
            Receiver = receiver;
        }
        public string Name { get; private set; }
        public UserInfo Sender { get; private set; }
        public InfoBase Receiver { get; private set; }
    }

    public abstract class CommandBase
    {
        protected CommandBase(IrcDaemon ircDaemon, string name, string p10)
        {
            IrcDaemon = ircDaemon;
            Name = name;
            P10Token = p10;
        }
        protected IrcDaemon IrcDaemon;
        public string Name { get; private set; }
        public string P10Token { get; private set; }
        public long CallCountIn { get; private set; }
        public long CallCountOut { get; private set; }
        public long ByteCountIn { get; private set; }
        public long ByteCountOut { get; private set; }

        abstract protected void PrivateHandle(UserInfo info, List<string> args);
        public void Handle(UserInfo info, List<string> args, int bytes)
        {
            if(bytes > 0)
            {
                CallCountIn++;
                ByteCountIn += bytes;
            }
            PrivateHandle(info, args);
        }

        abstract protected int PrivateSend(CommandArgument argument);
        public void Send(CommandArgument argument)
        {
            CallCountOut++;
            if(argument.Name == Name)
            {
                ByteCountOut += PrivateSend(argument);
            }
            else
            {
                // if the wrong Send is called, we requeue argument in the Servers CommandList
                IrcDaemon.Commands.Send(argument);
            }
        }

        protected readonly StringBuilder Command = new StringBuilder();
        protected void BuildMessageHeader(CommandArgument commandArgument)
        {
            Command.Length = 0;
            Command.Append(commandArgument.Sender == null ? IrcDaemon.ServerPrefix : commandArgument.Sender.Prefix);
            Command.Append(" ");
            Command.Append(commandArgument.Name);
            Command.Append(" ");
        }

        protected T GetSaveArgument<T>(CommandArgument commandArgument) where T : CommandArgument
        {
            var argument = commandArgument as T;
            if(argument == null)
            {
                throw new InvalidCastException("this shall not happen");
            }
            return argument;
        }

        public static string[] GetSubArgument(string arg)
        {
            return arg.Split(new[] { ',' });
        }

        /// <summary>
        ///Returns a list of ISUPPORT / Numeric 005 information this Mode is providing. 
        /// </summary>
        /// <param name="ircDaemon">Server Object</param>
        /// <returns>returns strings for direct usage in an ISUPPORT reply</returns>
        public virtual IEnumerable<string> Support(IrcDaemon ircDaemon)
        {
            return Enumerable.Empty<string>();
        }
    }
}