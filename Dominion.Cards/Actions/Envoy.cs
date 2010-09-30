using System;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Envoy : Card, IActionCard
    {
        public Envoy() : base(4)
        {
        }

        public void Play(TurnContext context)
        {
            var effect = new EnvoyEffect();
            context.AddEffect(effect);
        }

        private class EnvoyEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context)
            {
                var leftPlayer = context.Opponents.FirstOrDefault();
                if (leftPlayer != null)
                {
                    var revealZone = new RevealZone(context.ActivePlayer);
                    context.ActivePlayer.Deck.MoveTop(5, revealZone);
                    revealZone.LogReveal(context.Game.Log);

                    var activity = CreateChooseCardActivity(context, revealZone, leftPlayer);
                    _activities.Add(activity);
                }
            }

            private IActivity CreateChooseCardActivity(TurnContext context, RevealZone revealZone, Player player)
            {
                var selectTreasure = new SelectFromRevealedCardsActivity(context.Game.Log, player, revealZone,
                    string.Format("Select the card you do NOT want {0} to draw.", revealZone.Owner.Name), SelectionSpecifications.SelectExactlyXCards(1));

                selectTreasure.AfterCardsSelected = cards =>
                {
                    var discardingCard = cards.Single();
                    discardingCard.MoveTo(context.ActivePlayer.Discards);
                    revealZone.MoveAll(context.ActivePlayer.Hand);
                };

                return selectTreasure;
            }           
        }

        
    }
}