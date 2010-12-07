using System.Collections.Generic;
using Dominion.Cards.Treasure;
using Dominion.Rules;

namespace Dominion.GameHost.AI
{
    public static class Treasure
    {
        public static IList<string> Basic = GetBasicTreasure();

        private static IList<string> GetBasicTreasure()
        {
            var list = new List<string>();
            list.AddTreasure<Silver>();
            list.AddTreasure<Gold>();
            list.AddTreasure<Platinum>();
            return list;
        }

        private static void AddTreasure<T>(this IList<string> list) where T : Card
        {
            list.Add(typeof(T).Name);
        }
    }
}