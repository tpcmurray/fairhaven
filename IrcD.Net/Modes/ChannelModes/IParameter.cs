using System.Collections.Generic;
using IrcD.Channel;

namespace IrcD.Modes.ChannelModes
{
    public interface IParameter
    {
        string Add(string parameter);
    }

    public interface IParameterListA : IParameter
    {
        List<string> Parameter { get; }
        void SendList(UserInfo info, ChannelInfo chan);
        string Remove(string parameter);
    }

    public interface IParameterB : IParameter
    {
        string Parameter { get; set; }
    }

    public interface IParameterC : IParameter
    {
        string Parameter { get; set; }
    }
}