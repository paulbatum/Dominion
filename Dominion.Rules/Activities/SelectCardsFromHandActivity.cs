using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules.Activities
{
    public abstract class SelectCardsFromHandActivity : ActivityBase
    {
        protected SelectCardsFromHandActivity(IGameLog log, Player player, string message, ActivityType type, int count) 
            : base(log, player, message, type)
        {
            Count = count;
        }

        public int Count { get; private set; }

        public void SelectCards(IEnumerable<Card> cards)
        {
            if (cards.Count() != this.Count)
                throw new InvalidOperationException(
                    string.Format("Card count mismatch. Found {0} cards, expected {1}.", cards.Count(), this.Count));

            Execute(cards);

            IsSatisfied = true;
        }

        public abstract void Execute(IEnumerable<Card> cards);
    }
}