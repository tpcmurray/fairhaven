using System;

namespace IrcD.Utils
{
    public static class EnumExtension
    {
        public static string ToDescription(this Enum enumeration)
        {
            var type = enumeration.GetType();
            var memInfo = type.GetMember(enumeration.ToString());
            if(null != memInfo && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumDescription), false);
                if(null != attrs && attrs.Length > 0)
                    return ((EnumDescription)attrs[0]).Text;
            }
            return enumeration.ToString();
        }
    }
}