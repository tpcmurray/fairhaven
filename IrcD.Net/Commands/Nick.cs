using System.Collections.Generic;

namespace IrcD.Commands
{
    public class NickArgument : CommandArgument
    {
        public NickArgument(UserInfo sender, InfoBase receiver, string newNick)
            : base(sender, receiver, "NICK")
        {
            NewNick = newNick;
        }

        public string NewNick { get; private set; }
    }

    public class Nick : CommandBase
    {
        public Nick(IrcDaemon ircDaemon)
            : base(ircDaemon, "NICK", "N")
        { }

        protected override void PrivateHandle(UserInfo info, List<string> args)
        {
            if(!info.PassAccepted)
            {
                IrcDaemon.Replies.SendPasswordMismatch(info);
                return;
            }
            if(args.Count < 1)
            {
                IrcDaemon.Replies.SendNoNicknameGiven(info);
                return;
            }
            if(IrcDaemon.Nicks.ContainsKey(args[0]))
            {
                IrcDaemon.Replies.SendNicknameInUse(info, args[0]);
                return;
            }
            if(!IrcDaemon.ValidNick(args[0]))
            {
                IrcDaemon.Replies.SendErroneousNickname(info, args[0]);
                return;
            }
            // *** NICK command valid after this point ***
            if(!info.NickExists)
            {
                //First Nick Command
                IrcDaemon.Nicks.Add(args[0], info);
                info.InitNick(args[0]);
                return;
            }
            // Announce nick change to itself
            Send(new NickArgument(info, info, args[0]));
            // Announce nick change to all channels it is in
            foreach(var channelInfo in info.Channels)
            {
                Send(new NickArgument(info, channelInfo, args[0]));
            }
            info.Rename(args[0]);
        }

        protected override int PrivateSend(CommandArgument commandArgument)
        {
            var arg = GetSaveArgument<NickArgument>(commandArgument);
            BuildMessageHeader(arg);
            Command.Append(arg.NewNick);
            return arg.Receiver.WriteLine(Command);
        }
    }
}