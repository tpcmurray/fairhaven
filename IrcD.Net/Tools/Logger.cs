using System.Diagnostics;
using System;
using System.Text;

namespace IrcD.Utils
{
    class Logger
    {
        public static void Log(string message, int level = 4, string location = null)
        {
            var stackTrace = new StackTrace();
            var callerFrame = stackTrace.GetFrame(1);
            Console.WriteLine(string.Format("{0} in {2}: {1}", level, message, location ?? FormatLocation(callerFrame)));
        }

        public static string FormatLocation(StackFrame frame)
        {
            StringBuilder location = new StringBuilder();
            location.Append(frame.GetMethod().DeclaringType.ToString());
            location.Append("=>");
            location.Append(frame.GetMethod().ToString());
            location.Append(" [");
            location.Append(frame.GetILOffset());
            location.Append(":");
            location.Append(frame.GetNativeOffset());
            location.Append("]");
            return location.ToString();
        }
    }
}
