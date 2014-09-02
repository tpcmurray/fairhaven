using System.Collections.Generic;
using IrcD.Channel;
using IrcD.Utils;
using IrcD.Modes.ChannelRanks;

namespace IrcD.Commands
{
    public class KickArgument : CommandArgument
    {
        public KickArgument(UserInfo sender, InfoBase receiver, ChannelInfo channel, UserInfo user, string message)
            : base(sender, receiver, "KICK")
        {
            Channel = channel;
            User = user;
            Message = message;
        }

        public ChannelInfo Channel { get; private set; }
        public UserInfo User { get; private set; }
        public string Message { get; private set; }
    }

    public class Kick : CommandBase
    {
        public Kick(IrcDaemon ircDaemon)
            : base(ircDaemon, "KICK", "K")
        { }
        [CheckRegistered]
        [CheckParamCount(2)]
        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            var message = (args.Count > 2) ? args[2] : null;

            foreach(var subarg in GetSubArgument(args[0]).Zip(GetSubArgument(args[1]), (c, n) => new { Channel = c, Nick = n }))
            {
                if(!IrcDaemon.Channels.ContainsKey(subarg.Channel))
                {
                    IrcDaemon.Replies.SendNoSuchChannel(info, args[0]);
                    continue;
                }
                var chan = IrcDaemon.Channels[subarg.Channel];
                UserPerChannelInfo upci;
                if(chan.UserPerChannelInfos.TryGetValue(info.Nick, out upci))
                {
                    if(upci.RankList.Level < 30)
                    {
                        IrcDaemon.Replies.SendChannelOpPrivilegesNeeded(info, chan);
                        continue;
                    }
                }
                else
                {
                    IrcDaemon.Replies.SendNotOnChannel(info, chan.Name);
                    continue;
                }
                UserPerChannelInfo kickUser;
                if(chan.UserPerChannelInfos.TryGetValue(subarg.Nick, out kickUser))
                {
                    Send(new KickArgument(info, chan, chan, kickUser.UserInfo, message));
                    chan.UserPerChannelInfos.Remove(kickUser.UserInfo.Nick);
                    kickUser.UserInfo.UserPerChannelInfos.Remove(kickUser);
                }
                else
                {
                    IrcDaemon.Replies.SendUserNotInChannel(info, subarg.Channel, subarg.Nick);
                }
            }
        }
        protected override int PrivateSend(CommandArgument commandArgument)
        {
            var arg = GetSaveArgument<KickArgument>(commandArgument);
            BuildMessageHeader(arg);
            Command.Append(arg.Channel.Name);
            Command.Append(" ");
            Command.Append(arg.User.Nick);
            Command.Append(" :");
            Command.Append(arg.Message ?? IrcDaemon.Options.StandardKickMessage);
            return arg.Receiver.WriteLine(Command);
        }
    }
}