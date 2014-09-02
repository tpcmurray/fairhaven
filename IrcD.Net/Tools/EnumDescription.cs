using System;

namespace IrcD.Utils
{
    public class EnumDescription : Attribute
    {
        private readonly string text;
        public string Text
        {
            get { return text; }
        }
        public EnumDescription(string text)
        {
            this.text = text;
        }
    }
}
