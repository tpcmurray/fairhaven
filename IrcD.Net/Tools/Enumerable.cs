using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrcD.Utils
{
    public static class Enumerable
    {
        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> func)
        {
            var ie1 = first.GetEnumerator();
            var ie2 = second.GetEnumerator();
            while(ie1.MoveNext() && ie2.MoveNext())
                yield return func(ie1.Current, ie2.Current);
        }

        public static string Concatenate<T>(this IEnumerable<T> strings, string separator)
        {
            var stringBuilder = new StringBuilder();
            foreach(var item in strings)
            {
                stringBuilder.Append(item);
                stringBuilder.Append(separator);
            }
            stringBuilder.Length = stringBuilder.Length - separator.Length;
            return stringBuilder.ToString();
        }

        public static IEnumerable<EnumerableIndex<T>> EachIndex<T>(this IEnumerable<T> collection, int index = 0)
        {
            return collection.Select(value => new EnumerableIndex<T> { Value = value, Index = index++ });
        }
    }
}
