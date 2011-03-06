using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;

namespace Dominion.GameHost.AI
{
    public static class GameViewModelExtensions
    {
        public static ActivityHint ParseHint(this ActivityModel activity)
        {            
            return (ActivityHint) Enum.Parse(typeof (ActivityHint), activity.Hint);
        }

        public static ActivityType ParseType(this ActivityModel activity)
        {
            return (ActivityType)Enum.Parse(typeof(ActivityType), activity.Type);
        }

        public static int ParseNumberOfCardsToSelect(this ActivityModel activity)
        {
            return int.Parse(activity.Properties["NumberOfCardsToSelect"].ToString());
        }

        public static bool HasTypeRestriction(this ActivityModel activity)
        {
            return activity.Properties.ContainsKey("CardsMustBeOfType");
        }

        public static string ParseTypeRestriction(this ActivityModel activity)
        {
            return (string) activity.Properties["CardsMustBeOfType"];
        }

        public static IEnumerable<Choice> ParseOptions(this ActivityModel activity)
        {
            var options = (IEnumerable<string>)activity.Properties["AllowedOptions"];
            return options.Select(o => Enum.Parse(typeof (Choice), o))
                .Cast<Choice>();
        }

        public static bool Is(this CardViewModel card, CardType cardType)
        {
            return IsAny(card, new[] {cardType});
        }

        public static bool IsAny(this CardViewModel card, params CardType[] cardType)
        {
            return card.Types.Intersect(cardType.Select(ct => ct.ToString())).Any();
        }

        public static bool Is<T>(this CardViewModel card) where T : Card
        {
            return card.Name == typeof (T).Name;
        }

        public static bool Is(this CardPileViewModel pile, CardType cardType)
        {
            return IsAny(pile, new[] { cardType });
        }

        public static bool IsAny(this CardPileViewModel pile, params CardType[] cardType)
        {
            return pile.Types.Intersect(cardType.Select(ct => ct.ToString())).Any();
        }

        public static bool Is<T>(this CardPileViewModel pile) where T : Card
        {
            return pile.Name == typeof(T).Name;
        }

        public static CardPileViewModel Pile<T>(this IEnumerable<CardPileViewModel> bank) where T : Card
        {
            return bank.SingleOrDefault(pile => pile.Name == typeof (T).Name);
        }
    }
}