using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fairhaven
{
    public static class Strings
    {
        public static string Overwrite(this string str, string newstring, int start)
        {
            StringBuilder s = new StringBuilder(str);
            s.Remove(start, newstring.Length);
            s.Insert(start, newstring);
            return s.ToString();
        }

        public static string Repeat(this string str, int numToRepeat)
        {
            if(!string.IsNullOrEmpty(str))
            {
                StringBuilder builder = new StringBuilder(str.Length * numToRepeat);
                for(int i = 0; i < numToRepeat; i++) builder.Append(str);
                return builder.ToString();
            }
            return string.Empty;
        }
    }
}
