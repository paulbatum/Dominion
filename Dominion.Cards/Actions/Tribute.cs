using System.Linq;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Tribute : Card, IActionCard
    {
        public Tribute()
            : base(5)
        {

        }

        public void Play(TurnContext context)
        {
            var leftPlayer = context.Opponents.FirstOrDefault();
            if (leftPlayer != null)
            {
                var revealZone = new RevealZone(leftPlayer);
                leftPlayer.Deck.MoveTop(2, revealZone);
                revealZone.LogReveal(context.Game.Log);

                foreach (var card in revealZone.WithDistinctTypes())
                {
                    if (card is IActionCard) context.RemainingActions += 2;
                    if (card is ITreasureCard) context.AvailableSpend += 2;
                    if (card is IVictoryCard) context.DrawCards(2);
                }

                revealZone.MoveAll(leftPlayer.Discards);
            }


        }
    }
}