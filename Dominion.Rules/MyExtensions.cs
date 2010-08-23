using System;
using System.Collections.Generic;

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

    }
}