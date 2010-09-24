using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules.Activities
{
    public interface ISelectCardsActivity
    {
        void SelectCards(IEnumerable<Card> cards);
        ActivityType Type { get; }
    }

    public abstract class SelectCardsFromHandActivity : ActivityBase, IRestrictedActivity, ISelectCardsActivity
    {
        protected SelectCardsFromHandActivity(IGameLog log, Player player, string message, ActivityType type, int count) 
            : base(log, player, message, type)
        {
            Count = count;
            Restrictions = new List<RestrictionType>();
        }

        public int Count { get; private set; }
        public IList<RestrictionType> Restrictions { get; private set; }

        public virtual void SelectCards(IEnumerable<Card> cards)
        {
            if (cards.Count() != this.Count)
                throw new InvalidOperationException(
                    string.Format("Card count mismatch. Found {0} cards, expected {1}.", cards.Count(), this.Count));
            this.EnsureCardsAreAllowed(cards);

            Execute(cards);

            IsSatisfied = true;
        }

        public abstract void Execute(IEnumerable<Card> cards);
    }

    public abstract class SelectAnyNumberOfCardsFromHandActivity : ActivityBase, IRestrictedActivity, ISelectCardsActivity
    {
        protected SelectAnyNumberOfCardsFromHandActivity(IGameLog log, Player player, string message)
            : base(log, player, message, ActivityType.SelectAnyNumberOfCards)
        {
            Restrictions = new List<RestrictionType>();
        }

        public IList<RestrictionType> Restrictions { get; private set; }

        public virtual void SelectCards(IEnumerable<Card> cards)
        {            
            this.EnsureCardsAreAllowed(cards);
            Execute(cards);
            IsSatisfied = true;
        }

        public abstract void Execute(IEnumerable<Card> cards);
    }
}