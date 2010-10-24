using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules
{
    public static class MyExtensions
    {
        public static void Times(this int count, Action action)
        {
            if (action != null)
            {
                for (int i = 1; i <= count; i++)
                    action();
            }
        }

        public static IEnumerable<T> Items<T>(this int count, Func<T> builder)
        {
            if (builder == null)
                yield break;

            for (int i = 1; i <= count; i++)
                yield return builder();
        }

        public static IEnumerable<T> Items<T>(this int count, Func<int, T> builder)
        {
            if (builder == null)
                yield break;

            for (int i = 1; i <= count; i++)
                yield return builder(i);
        }

        public static IEnumerable<T> WithDistinctTypes<T>(this IEnumerable<T> input)
        {
            return input.Distinct(new SameTypeEqualityComparer<T>());
        }

        public static bool AllSame<T,T2>(this IEnumerable<T> items, Func<T, T2> selector)
        {            
            return items.Select(selector).Distinct().Count() < 2;
        }
    }
}