using System.Collections.Generic;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public static class ProbabilityExtensions
    {
        public static IEnumerable<KeyValuePair<T, int>> GetCumulativeTotals<T>(this IEnumerable<KeyValuePair<T, int>> input)
        {
            var temp = 0;
            foreach (var item in input)
            {
                yield return new KeyValuePair<T, int>(item.Key, item.Value + temp);
                temp += item.Value;
            }
        }

    }
}