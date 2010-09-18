using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Hybrid
{
    public class GreatHall : ActionCard, IScoreCard
    {
        public GreatHall() : base(3)
        {
            Value = 1;
        }

        protected override void Play(TurnContext context)
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
    }
}
