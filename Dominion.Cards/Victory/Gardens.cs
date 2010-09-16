using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Victory
{
    public class Gardens : VictoryCard
    {

        public Gardens()
            : base(0,4)
        {
        }

        public override int Score(Dominion.Rules.CardZone allCards)
        {
            return (allCards.CardCount / 10);
        }

    }
}
