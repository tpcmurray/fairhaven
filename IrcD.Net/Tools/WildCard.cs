/*
 *The ircd.net project is an IRC deamon implementation for the .NET Plattform
 *It should run on both .NET and Mono
 * 
 *Copyright(c) 2008-2010 Thomas Bruderer <apophis@apophis.ch>
 *Copyright(c) 2005-2009 Davide Icardi, reinux
 *
 *http://www.codeproject.com/KB/recipes/wildcardtoregex.aspx
 *
 *No explicit license, and GPL 3 in this Project
 */
using System;
using System.Text.RegularExpressions;
namespace IrcD.Utils
{
    public class WildCard : Regex
    {
        public WildCard(string pattern, WildcardMatch matchType)
            : base(WildcardToRegex(pattern, matchType))
        {
        }

        public WildCard(string pattern, RegexOptions options, WildcardMatch matchType)
            : base(WildcardToRegex(pattern, matchType), options)
        {
        }

        public static string WildcardToRegex(string pattern, WildcardMatch matchType)
        {
            string escapedPattern = Escape(pattern);
            escapedPattern = escapedPattern.Replace("\\*", ".*");
            escapedPattern = escapedPattern.Replace("\\?", ".");
            switch(matchType)
            {
                case WildcardMatch.Exact:
                    return escapedPattern;
                case WildcardMatch.Anywhere:
                    return escapedPattern + "$";
                case WildcardMatch.StartsWith:
                    return "^" + escapedPattern;
                case WildcardMatch.EndsWith:
                    return "^" + escapedPattern + "$";
                default:
                    throw new ArgumentOutOfRangeException("matchType");
            }
        }
    }

    public enum WildcardMatch
    {
        Exact = 0,
        Anywhere = 1,
        StartsWith = 2,
        EndsWith = 3
    }
}