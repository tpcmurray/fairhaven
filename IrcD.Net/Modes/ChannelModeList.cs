using System.Collections.Generic;
using System.Linq;
using System.Text;
using IrcD.Channel;
using IrcD.Commands;
using IrcD.Modes.ChannelModes;

namespace IrcD.Modes
{
    public class ChannelModeList : ModeList<ChannelMode>
    {
        public ChannelModeList(IrcDaemon ircDaemon) : base(ircDaemon) { }

        public bool IsSecret()
        {
            return Values.Any(mode => mode is ModeSecret);
        }

        public bool IsPrivate()
        {
            return Values.Any(mode => mode is ModePrivate);
        }

        /// <returns>returns true if all Modes return true and therefore don't stop the execution of the Command</returns>
        public bool HandleEvent(CommandBase command, ChannelInfo channel, UserInfo user, List<string> args)
        {
            return Values.All(mode => mode.HandleEvent(command, channel, user, args));
        }

        internal void Update(UserInfo info, ChannelInfo chan, IEnumerable<string> args)
        {
            // In
            var sentPrivNeeded = false;
            var plus = (args.First().Length == 1) ? (bool?)null : true;
            var parameterTail = args.Skip(1);
            // Out: this is the final mode message
            var lastprefix = ' ';
            var validmode = new StringBuilder();
            var validparam = new List<string>();
            foreach(var modechar in args.First())
            {
                if(modechar == '+' || modechar == '-')
                {
                    plus = (modechar == '+');
                    continue;
                }
                var cmode = IrcDaemon.ModeFactory.GetChannelMode(modechar);
                if(cmode != null && !chan.UserPerChannelInfos[info.Nick].RankList.CurrentRank.CanChangeChannelMode(cmode))
                {
                    if(!sentPrivNeeded)
                    {
                        IrcDaemon.Replies.SendChannelOpPrivilegesNeeded(info, chan);
                        sentPrivNeeded = true;
                    }
                    continue;
                }
                var crank = IrcDaemon.ModeFactory.GetChannelRank(modechar);
                if(crank != null && !chan.UserPerChannelInfos[info.Nick].RankList.CurrentRank.CanChangeChannelRank(crank))
                {
                    if(!sentPrivNeeded)
                    {
                        IrcDaemon.Replies.SendChannelOpPrivilegesNeeded(info, chan);
                        sentPrivNeeded = true;
                    }
                    continue;
                }
                if(plus == null)
                {
                    var list = cmode as IParameterListA;
                    if(list != null)
                    {
                        ChannelMode channelMode;
                        if(TryGetValue(cmode.Char, out channelMode))
                        {
                            ((IParameterListA)channelMode).SendList(info, chan);
                        }
                        else
                        {
                            //No list yet, Send empty List
                            list.SendList(info, chan);
                        }
                        return;
                    }
                    plus = true;
                }

                var iParam = cmode as IParameter;
                if(iParam != null)
                {
                    var parameter = parameterTail.FirstOrDefault();
                    if(parameter != null)
                    {
                        parameterTail = parameterTail.Skip(1);
                        if(plus.Value)
                        {
                            if(!ContainsKey(cmode.Char))
                            {
                                Add(cmode);
                            }
                            if(lastprefix != '+')
                            {
                                validmode.Append(lastprefix = '+');
                            }
                            validmode.Append(cmode.Char);
                            validparam.Add(((IParameter)this[cmode.Char]).Add(parameter));
                        }
                        else
                        {
                            if(ContainsKey(cmode.Char))
                            {
                                var paramA = this[cmode.Char] as IParameterListA;
                                if(paramA == null)
                                {
                                    Remove(cmode.Char);
                                    if(lastprefix != '-')
                                    {
                                        validmode.Append(lastprefix = '-');
                                    }
                                    validmode.Append(cmode.Char);
                                    validparam.Add(parameter);
                                }
                                else
                                {
                                    var p = paramA.Remove(parameter);
                                    if(lastprefix != '-')
                                    {
                                        validmode.Append(lastprefix = '-');
                                    }
                                    if(p != null)
                                    {
                                        validmode.Append(cmode.Char);
                                        validparam.Add(p);
                                    }
                                }
                            }
                        }
                    }
                }
                else if(cmode != null)
                {
                    // Channel Mode without a parameter
                    if(plus.Value)
                    {
                        if(!ContainsKey(cmode.Char))
                        {
                            Add(cmode);
                            if(lastprefix != '+')
                            {
                                validmode.Append(lastprefix = '+');
                            }
                            validmode.Append(cmode.Char);
                        }
                    }
                    else
                    {
                        if(ContainsKey(cmode.Char))
                        {
                            Remove(cmode.Char);
                            if(lastprefix != '-')
                            {
                                validmode.Append(lastprefix = '-');
                            }
                            validmode.Append(cmode.Char);
                        }
                    }
                }
                else if(crank != null)
                {
                    var parameter = parameterTail.FirstOrDefault();
                    if(parameter != null)
                    {
                        UserPerChannelInfo upci;
                        parameterTail = parameterTail.Skip(1);
                        if(chan.UserPerChannelInfos.TryGetValue(parameter, out upci))
                        {
                            if(plus.Value)
                            {
                                if(!upci.RankList.ContainsKey(crank.Char))
                                {
                                    upci.RankList.Add(crank);
                                    if(lastprefix != '+')
                                    {
                                        validmode.Append(lastprefix = '+');
                                    }
                                    validmode.Append(crank.Char);
                                    validparam.Add(parameter);
                                }
                            }
                            else
                            {
                                if(upci.RankList.ContainsKey(crank.Char))
                                {
                                    upci.RankList.Remove(crank.Char);
                                    if(lastprefix != '-')
                                    {
                                        validmode.Append(lastprefix = '-');
                                    }
                                    validmode.Append(crank.Char);
                                    validparam.Add(parameter);
                                }
                            }
                        }
                        else
                        {
                            info.IrcDaemon.Replies.SendUserNotInChannel(info, chan.Name, parameter);
                        }
                    }
                }
                else
                {
                    info.IrcDaemon.Replies.SendUnknownMode(info, chan, modechar);
                }
            }

            // Integrate Parameters into final mode string
            foreach(var param in validparam)
            {
                validmode.Append(" ");
                validmode.Append(param);
            }
            info.IrcDaemon.Commands.Send(new ModeArgument(info, chan, chan.Name, validmode.ToString()));
        }

        public string ToParameterList()
        {
            var modes = new StringBuilder();
            foreach(var mode in Values.Where(m => m is IParameterListA))
            {
                modes.Append(mode.Char);
            }
            modes.Append(',');
            foreach(var mode in Values.Where(m => m is IParameterB))
            {
                modes.Append(mode.Char);
            }
            modes.Append(',');
            foreach(var mode in Values.Where(m => m is IParameterC))
            {
                modes.Append(mode.Char);
            }
            modes.Append(',');
            foreach(var mode in Values.Where(m => !(m is IParameterListA) && !(m is IParameterB) && !(m is IParameterC)))
            {
                modes.Append(mode.Char);
            }
            return modes.ToString();
        }

        public string ToChannelModeString()
        {
            var modes = new StringBuilder("+");
            var parameters = new StringBuilder();
            foreach(var mode in Values.Where(m => !(m is IParameterListA)).OrderBy(c => c.Char))
            {
                modes.Append(mode.Char);
                if(mode is IParameterB)
                {
                    parameters.Append(" ");
                    parameters.Append(((IParameterB)mode).Parameter);
                }
                if(mode is IParameterC)
                {
                    parameters.Append(" ");
                    parameters.Append(((IParameterC)mode).Parameter);
                }
            }
            return modes.ToString() + parameters;
        }
    }
}