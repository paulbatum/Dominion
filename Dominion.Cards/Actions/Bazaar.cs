using System.Text;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Bazaar : Card, IActionCard
    {
        public Bazaar() : base(5)
        {
        }
        
        public void Play(TurnContext context)
        {
            context.DrawCards(1);
            context.RemainingActions += 2;
            context.AvailableSpend += 1;
        }
 
    }
}
