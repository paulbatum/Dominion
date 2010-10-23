using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class City : Card, IActionCard
    {
        public City()
            : base(5)
        {
        }

        public void Play(TurnContext context)
        {
            context.RemainingActions += 2;
            context.DrawCards(1);

            var emptyPileCount = context.Game.Bank.EmptyPileCount;
            
            if(emptyPileCount >= 1)
                context.DrawCards(1);

            if(emptyPileCount >= 2)
            {
                context.AvailableSpend += 1;
                context.Buys += 1;
            }

        }
    }
}