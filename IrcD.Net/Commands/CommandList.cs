using System;
using System.Collections.Generic;
using System.Linq;
using IrcD.ServerReplies;
#if DEBUG
using System.Text;
using IrcD.Utils;
#endif

namespace IrcD.Commands
{
    public class CommandList : IEnumerable<CommandBase>
    {
        private readonly Dictionary<string, CommandBase> commandList;
        private readonly IrcDaemon ircDaemon;

        public CommandList(IrcDaemon ircDaemon)
        {
            commandList = new Dictionary<string, CommandBase>(StringComparer.OrdinalIgnoreCase);
            this.ircDaemon = ircDaemon;
        }

        public void Add(CommandBase command)
        {
            commandList.Add(command.Name, command);
        }

        public IEnumerable<string> Supported()
        {
            return commandList.SelectMany(m => m.Value.Support(ircDaemon));
        }

        public void Handle(UserInfo info, string prefix, ReplyCode replyCode, List<string> args, int bytes)
        {
            throw new NotImplementedException();
        }

        public void Handle(UserInfo info, string prefix, string command, List<string> args, int bytes)
        {
            CommandBase commandObject;
            if(commandList.TryGetValue(command, out commandObject))
            {
                bool skipHandle = false;
                var handleMethodInfo = commandObject.GetType().GetMethod("Handle");
                var checkRegistered = Attribute.GetCustomAttribute(handleMethodInfo, typeof(CheckRegisteredAttribute)) as CheckRegisteredAttribute;
                if(checkRegistered != null)
                {
                    if(!info.Registered)
                    {
                        ircDaemon.Replies.SendNotRegistered(info);
                        skipHandle = true;
                    }
                }
                var checkParamCount = Attribute.GetCustomAttribute(handleMethodInfo, typeof(CheckParamCountAttribute)) as CheckParamCountAttribute;
                if(checkParamCount != null)
                {
                    if(args.Count < checkParamCount.MinimumParameterCount)
                    {
                        ircDaemon.Replies.SendNeedMoreParams(info);
                        skipHandle = true;
                    }
                }

                if(!skipHandle)
                {
                    commandObject.Handle(info, args, bytes);
                    info.LastAlive = DateTime.Now;
                }
            }
            else
            {
#if DEBUG
                Logger.Log("Command " + command + "is not yet implemented");
#endif
                if(info.Registered)
                {
                    // we only inform the client about invalid commands if he is already successfully registered
                    // we dont want to make "wrong protocol ping-pong"
                    ircDaemon.Replies.SendUnknownCommand(info, command);
                }
            }
#if DEBUG
            var parsedLine = new StringBuilder();
            parsedLine.Append("[" + info.Usermask + "]-[" + command + "]");
            foreach(var arg in args)
            {
                parsedLine.Append("-<" + arg + ">");
            }
            Logger.Log(parsedLine.ToString());
#endif
        }

        public void Send(CommandArgument argument)
        {
            CommandBase commandObject = commandList[argument.Name];
            commandObject.Send(argument);
        }

        public IEnumerator<CommandBase> GetEnumerator()
        {
            return commandList.Select(command => command.Value).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return commandList.Select(command => command.Value).GetEnumerator();
        }
    }
}