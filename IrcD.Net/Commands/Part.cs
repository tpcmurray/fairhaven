using System.Collections.Generic;
using System.Linq;
using IrcD.Channel;

namespace IrcD.Commands
{
    public class PartArgument : CommandArgument
    {
        public PartArgument(UserInfo sender, InfoBase receiver, ChannelInfo channel, string message)
            : base(sender, receiver, "PART")
        {
            Channel = channel;
            Message = message;
        }
        public ChannelInfo Channel { get; private set; }
        public string Message { get; private set; }
    }

    public class Part : CommandBase
    {
        public Part(IrcDaemon ircDaemon)
            : base(ircDaemon, "PART", "L")
        { }
        [CheckRegistered]
        [CheckParamCount(1)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            var message = (args.Count > 1) ? args[1] : IrcDaemon.Options.StandardPartMessage;

            foreach(string ch in GetSubArgument(args[0]))
            {
                if(IrcDaemon.Channels.ContainsKey(ch))
                {
                    var chan = IrcDaemon.Channels[ch];
                    var upci = chan.UserPerChannelInfos[info.Nick];
                    if(info.Channels.Contains(chan))
                    {
                        Send(new PartArgument(info, chan, chan, message));
                        chan.UserPerChannelInfos.Remove(info.Nick);
                        info.UserPerChannelInfos.Remove(upci);
                        if(!chan.UserPerChannelInfos.Any())
                        {
                            IrcDaemon.Channels.Remove(chan.Name);
                        }
                    }
                    else
                    {
                        IrcDaemon.Replies.SendNotOnChannel(info, ch);
                        continue;
                    }
                }
                else
                {
                    IrcDaemon.Replies.SendNoSuchChannel(info, ch);
                    continue;
                }
            }
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            var arg = GetSaveArgument<PartArgument>(commandArgument);

            BuildMessageHeader(arg);
            Command.Append(arg.Channel.Name);
            Command.Append(" :");
            Command.Append(arg.Message);
            return arg.Receiver.WriteLine(Command);
        }
    }
}