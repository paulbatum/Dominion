using System.Collections.Generic;
using Dominion.Cards.Victory;
using Dominion.Rules;

namespace Dominion.GameHost.AI
{
    public static class VictoryCards
    {
        public static IList<string> Basic = GetBasicVP();

        private static IList<string> GetBasicVP()
        {
            var list = new List<string>();
            list.AddCard<Estate>();
            list.AddCard<Duchy>();
            list.AddCard<Province>();
            list.AddCard<Colony>();
            return list;
        }

        private static void AddCard<T>(this IList<string> list) where T : Card
        {
            list.Add(typeof(T).Name);
        }
    }
}