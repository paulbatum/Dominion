using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules.Activities
{

    public interface ISelectionSpecification
    {
        bool IsMatch(IEnumerable<Card> cards);
        ActivityType ActivityType { get; }
        Type CardTypeRestriction { get; set; }
        void WriteProperties(IDictionary<string, object> bag);
    }

    public static class SelectionSpecifications
    {
        public static ISelectionSpecification SelectExactlyXCards(int cardCount)
        {
            return new SelectionSpecification
            {
                MatchFunction = cards => cards.Count() == cardCount,
                ActivityType = ActivityType.SelectFixedNumberOfCards,
                Count = cardCount
            };
        }

        public static ISelectionSpecification SelectAnyNumberOfCards()
        {
            return new SelectionSpecification
            {                
                ActivityType = ActivityType.SelectAnyNumberOfCards
            };
        }

        public static ISelectionSpecification SelectUpToXCards(int upTo)
        {
            return new SelectionSpecification
            {
                MatchFunction = cards => cards.Count() <= upTo,
                ActivityType = ActivityType.SelectUpToNumberOfCards,
                Count = upTo
            };
        }

        private class SelectionSpecification : ISelectionSpecification
        {            
            public Func<IEnumerable<Card>, bool> MatchFunction { get; set; }
            public ActivityType ActivityType { get; set; }
            public Type CardTypeRestriction { get; set; }
            public int? Count { get; set; }

            public void WriteProperties(IDictionary<string, object> bag)
            {
                if(Count != null)
                    bag["NumberOfCardsToSelect"] = Count;

                if (CardTypeRestriction != null)
                    bag["CardsMustBeOfType"] = this.CardTypeRestriction.Name;
            }

            public bool IsMatch(IEnumerable<Card> cards)
            {
                if (CardTypeRestriction != null && 
                    cards.All(c => c.GetType().GetInterfaces().Contains(CardTypeRestriction)) == false)
                    return false;                                

                if(MatchFunction != null)
                    return MatchFunction(cards);

                return true;
            }           
        }
    }
}