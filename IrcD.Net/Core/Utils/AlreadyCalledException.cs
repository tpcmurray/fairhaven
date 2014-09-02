using System;

namespace IrcD
{
    /// <summary>
    /// This Exception is thrown for methods which only should be called once, but are called a second time.
    /// </summary>
    public class AlreadyCalledException : Exception
    {
    }
}