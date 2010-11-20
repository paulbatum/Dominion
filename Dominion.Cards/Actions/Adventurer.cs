using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Adventurer : Card, IActionCard
    {
        public Adventurer() : base(6)
        {
            
        }

        public void Play(TurnContext context)
        {
            context.AddEffect(new AdventurerEffect());                                
        }

        private class AdventurerEffect : CardEffectBase
        {
            private IEnumerable<ITreasureCard> MatchingCards(RevealZone zone)
            {
                return zone.OfType<ITreasureCard>();
            }

            public override void Resolve(TurnContext context)
            {
                var deck = context.ActivePlayer.Deck;
                var revealZone = new RevealZone(context.ActivePlayer);

                while (deck.TopCard != null && MatchingCards(revealZone).Count() < 2)
                    deck.TopCard.MoveTo(revealZone);

                revealZone.LogReveal(context.Game.Log);
                var revealedTreasure = MatchingCards(revealZone).ToList();

                var discards = revealZone.Where(c => !revealedTreasure.Cast<ICard>().Contains(c)).ToList();

                foreach(var card in discards)
                    card.MoveTo(context.ActivePlayer.Discards);

                foreach (var card in revealedTreasure)
                    card.MoveTo(context.ActivePlayer.Hand);

            }
        }
    }
}