﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using IrcD.Channel;
using IrcD.Modes;
using IrcD.Utils;
using IrcD.Commands;
namespace IrcD
{
    public class QuserInfo : UserInfo
    {
        public QuserInfo(IrcDaemon ircDaemon) : base(ircDaemon, null, null, true, true)
        {
            InitNick("Q");
        }

        public override string Prefix
        {
            get
            {
                return ":" + Nick;
            }
        }
    }

    public class UserInfo : InfoBase
    {
        public UserInfo(IrcDaemon ircDaemon, Socket socket, string host, bool isAcceptSocket, bool passAccepted)
            : base(ircDaemon)
        {
            IsService = false;
            Registered = false;
            PassAccepted = passAccepted;
            Host = host;
            Created = DateTime.Now;
            this.isAcceptSocket = isAcceptSocket;
            this.socket = socket;
            modes = new UserModeList(ircDaemon);
        }

        private readonly Socket socket;
        internal Socket Socket
        {
            get
            {
                return socket;
            }
        }

        private readonly bool isAcceptSocket;
        public bool IsAcceptSocket
        {
            get
            {
                return isAcceptSocket;
            }
        }
        public bool PassAccepted { get; internal set; }
        public bool Registered { get; internal set; }
        public bool IsService { get; set; }
        public string User { get; private set; }
        public string Nick { get; private set; }
        public string RealName { get; private set; }
        public string Host { get; private set; }
        public string AwayMessage { get; set; }

        public List<string> Capabilities { get; private set; }
        private IEnumerable<string> languages = new List<string> { "en" };
        public IEnumerable<string> Languages
        {
            get
            {
                return languages.Any() ? languages : System.Linq.Enumerable.Repeat("en", 1);
            }
            set
            {
                languages = value.Where(l => GoogleTranslate.Languages.ContainsKey(l));
            }
        }

        public void InitNick(string nick)
        {
            if(NickExists)
            {
                throw new AlreadyCalledException();
            }
            Nick = nick;
            if(!UserExists) return;
            RegisterComplete();
        }
        public void InitUser(string user, string realname)
        {
            if(UserExists)
            {
                throw new AlreadyCalledException();
            }
            User = user;
            RealName = realname;
            if(!NickExists) return;
            RegisterComplete();
        }
        private void RegisterComplete()
        {
            Logger.Log(string.Format("New User: {0}", this.Usermask));
            Registered = true;
            if(IsService)
            {
                IrcDaemon.Replies.SendYouAreService(this);
                IrcDaemon.Replies.SendYourHost(this);
                IrcDaemon.Replies.SendMyInfo(this);
            }
            else
            {
                IrcDaemon.Replies.RegisterComplete(this);
            }
        }

        internal bool UserExists
        {
            get
            {
                return User != null;
            }
        }
        public bool NickExists
        {
            get
            {
                return Nick != null;
            }
        }
        public void Rename(string newNick)
        {
            // Update Global Nick-Dictionary
            IrcDaemon.Nicks.Remove(Nick);
            IrcDaemon.Nicks.Add(newNick, this);
            // Update Channel Nicklists
            foreach(var channel in Channels)
            {
                var channelInfo = channel.UserPerChannelInfos[Nick];
                channel.UserPerChannelInfos.Remove(Nick);
                channel.UserPerChannelInfos.Add(newNick, channelInfo);
            }
            Nick = newNick;
        }
        public string Usermask
        {
            get
            {
                return Nick + "!" + User + "@" + Host;
            }
        }
        public virtual string Prefix
        {
            get
            {
                return ":" + Usermask;
            }
        }
        private DateTime lastAction = DateTime.Now;
        public DateTime LastAction
        {
            get
            {
                return lastAction;
            }
            set
            {
                lastAction = value;
            }
        }
        public DateTime LastAlive { get; set; }
        public DateTime LastPing { get; set; }

        private readonly List<UserPerChannelInfo> userPerChannelInfos = new List<UserPerChannelInfo>();
        public List<UserPerChannelInfo> UserPerChannelInfos
        {
            get
            {
                return userPerChannelInfos;
            }
        }
        public IEnumerable<ChannelInfo> Channels
        {
            get
            {
                return userPerChannelInfos.Select(upci => upci.ChannelInfo);
            }
        }
        private readonly List<ChannelInfo> invited = new List<ChannelInfo>();
        public List<ChannelInfo> Invited
        {
            get
            {
                return invited;
            }
        }
        private readonly UserModeList modes;
        public UserModeList Modes
        {
            get
            {
                return modes;
            }
        }
        public string ModeString
        {
            get
            {
                return modes.ToUserModeString();
            }
        }
        public DateTime Created { get; private set; }
        public override string ToString()
        {
            return Nick + "!" + User + "@" + Host;
        }
        public override int WriteLine(StringBuilder line)
        {
#if DEBUG
            Logger.Log(line.ToString(), location: "OUT:" + Nick);
#endif
            return socket.Send(Encoding.UTF8.GetBytes(line + IrcDaemon.ServerCrLf));
        }
        public override int WriteLine(StringBuilder line, UserInfo exception)
        {
            if(this != exception)
            {
                return WriteLine(line);
            }
            return 0;
        }
        // Cleanly Quit a user, in any case, Connection dropped, QuitMesssage, all traces of 'this' mus be removed.
        public void Remove(string message)
        {
            // Clean up channels
            foreach(var upci in UserPerChannelInfos)
            {
                // Important: remove nick first! or we end in a exception-catch endless loop
                upci.ChannelInfo.UserPerChannelInfos.Remove(Nick);
                IrcDaemon.Commands.Send(new QuitArgument(this, upci.ChannelInfo, message));
            }
            UserPerChannelInfos.Clear();
            // Clean up server
            if(Nick != null && IrcDaemon.Nicks.ContainsKey(Nick))
            {
                IrcDaemon.Nicks.Remove(Nick);
            }
            if(IrcDaemon.Sockets.ContainsKey(socket))
            {
                IrcDaemon.Sockets.Remove(socket);
            }
            // Close connection
            socket.Close();
            // Ready for destruction 
        }
        internal static string NormalizeHostmask(string parameter)
        {
            var hasAt = parameter.Contains('@');
            var hasEx = parameter.Contains('!');
            if(!hasAt && !hasEx)
            {
                if(parameter.Contains('.'))
                {
                    parameter = "*!*@" + parameter;
                }
                else
                {
                    parameter = parameter + "!*@*";
                }
            }
            if(hasEx && parameter.First() == '!')
            {
                parameter = "*" + parameter;
            }
            if(hasAt && parameter.Last() == '@')
            {
                parameter = parameter + "*";
            }
            return parameter;
        }
    }
}