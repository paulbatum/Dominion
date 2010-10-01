using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Expand : Card, IActionCard
    {
        public Expand() : base(7)
        {
        }

        public void Play(TurnContext context)
        {
            context.AddEffect(new ExpandEffect());
        }
        
        public class ExpandEffect : Remodel.RemodelEffect
        {
            public ExpandEffect()
                :base(3, "Select a card to Expand.")
            {
                
            }
        }
    }
}