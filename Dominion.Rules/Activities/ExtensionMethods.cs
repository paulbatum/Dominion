using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules.Activities
{
    public static class ExtensionMethods
    {
        public static void EnsureCardsAreAllowed(this IRestrictedActivity activity, IEnumerable<Card> cardList)
        {
            foreach (var card in cardList)
                activity.EnsureCardIsAllowed(card);
        }

        public static void EnsureCardIsAllowed(this IRestrictedActivity activity, Card card)
        {
            bool cardAllowed = false;
            if (activity.Restrictions.Count() == 0)
                cardAllowed |= true;

            if (card is IActionCard)
                cardAllowed |= activity.Restrictions.Contains(RestrictionType.ActionCard);
            if (card is ITreasureCard)
                cardAllowed |= activity.Restrictions.Contains(RestrictionType.TreasureCard);
            if (card is IReactionCard)
                cardAllowed |= activity.Restrictions.Contains(RestrictionType.ReactionCard);

            if (cardAllowed)
                return;

            throw new ArgumentException("Card is disallowed by restrictions.", "card");
        }
    }
}
