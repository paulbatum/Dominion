using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Hybrid
{
    public class GreatHall : Card, IActionCard, IVictoryCard
    {
        public GreatHall() : base(3)
        {
            Value = 1;
        }

        public void Play(TurnContext context)
        {
            context.DrawCards(1);
            context.RemainingActions += 1;
        }

        public int Value
        {
            get;
            set;
        }

        public int Score(CardZone allCards)
        {
            return Value;
        }

        public void PlayFromOtherAction(TurnContext context)
        {
            Play(context);
        }
    }
}
