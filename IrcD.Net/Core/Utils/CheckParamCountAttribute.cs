using System;

namespace IrcD
{
    public class CheckParamCountAttribute : Attribute
    {
        readonly int minimumParameterCount;
        public int MinimumParameterCount
        {
            get
            {
                return minimumParameterCount;
            }
        }

        public CheckParamCountAttribute(int minimumParameterCount)
        {
            this.minimumParameterCount = minimumParameterCount;
        }
    }
}