using System.Collections.Generic;
using IrcD.Channel;

namespace IrcD.Commands
{
    public class TopicArgument : CommandArgument
    {
        public TopicArgument(UserInfo sender, InfoBase receiver, ChannelInfo channel, string newTopic)
            : base(sender, receiver, "TOPIC")
        {
            Channel = channel;
            NewTopic = newTopic;
        }

        public ChannelInfo Channel { get; private set; }
        public string NewTopic { get; private set; }
    }
    
    public class Topic : CommandBase
    {
        public Topic(IrcDaemon ircDaemon)
            : base(ircDaemon, "TOPIC", "T")
        { }

        [CheckRegistered]
        [CheckParamCount(1)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            if(!IrcDaemon.Channels.ContainsKey(args[0]))
            {
                IrcDaemon.Replies.SendNoSuchChannel(info, args[0]);
                return;
            }
            var chan = IrcDaemon.Channels[args[0]];
            if(args.Count == 1)
            {
                if(string.IsNullOrEmpty(chan.Topic))
                {
                    IrcDaemon.Replies.SendNoTopicReply(info, chan);
                }
                else
                {
                    IrcDaemon.Replies.SendTopicReply(info, chan);
                }
                return;
            }
            chan.Topic = args[1];
            // Some Mode might want to handle the command
            if(!chan.Modes.HandleEvent(this, chan, info, args))
            {
                return;
            }
            foreach(var user in chan.Users)
            {
                Send(new TopicArgument(info, user, chan, chan.Topic));
            }
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            var arg = GetSaveArgument<TopicArgument>(commandArgument);
            BuildMessageHeader(arg);
            Command.Append(arg.Channel.Name);
            Command.Append(" :");
            Command.Append(arg.NewTopic);
            return arg.Receiver.WriteLine(Command);
        }
    }
}