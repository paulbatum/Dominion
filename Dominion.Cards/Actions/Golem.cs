using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Golem : Card, IActionCard
    {
        public Golem() : base(new CardCost(4, 1))
        {

        }

        public void Play(TurnContext context)
        {
            context.AddEffect(this, new GolemEffect());                                
        }

        private class GolemEffect : CardEffectBase
        {
            private IEnumerable<IActionCard> MatchingActions(RevealZone zone)
            {
                return zone.OfType<IActionCard>().Where(c => !(c is Golem));
            }

            public override void Resolve(TurnContext context, ICard source)
            {
                var deck = context.ActivePlayer.Deck;
                var revealZone = new RevealZone(context.ActivePlayer);

                while (deck.TopCard != null && MatchingActions(revealZone).Count() < 2)
                    deck.TopCard.MoveTo(revealZone);

                revealZone.LogReveal(context.Game.Log);
                var actionsToPlay = MatchingActions(revealZone).ToList();

                var discards = revealZone.Where(c => !actionsToPlay.Cast<ICard>().Contains(c))
                    .ToList();

                foreach(var card in discards)
                    card.MoveTo(context.ActivePlayer.Discards);

                var playUtil = new PlayCardUtility(context);

                if (actionsToPlay.AllSame(a => a.Name))
                    playUtil.Play(actionsToPlay);                                       
                else
                    _activities.Add(CreateChooseActionActivity(context, revealZone, source));
            }

            private IActivity CreateChooseActionActivity(TurnContext context, RevealZone revealZone, ICard source)
            {
                var selectTreasure = new SelectFromRevealedCardsActivity(context.Game.Log, context.ActivePlayer, revealZone,
                    "Select the action to play first.", SelectionSpecifications.SelectExactlyXCards(1), source);

                selectTreasure.AfterCardsSelected = cards =>
                {
                    var first = cards.OfType<IActionCard>().Single();
                    var second = revealZone.OfType<IActionCard>().Single(c => c != first);

                    // Reverse order because we're on a stack.
                    context.AddEffect(source, new PlayCardEffect(second));
                    context.AddEffect(source, new PlayCardEffect(first));                    
                };

                return selectTreasure;
            }  

            private class PlayCardEffect : CardEffectBase
            {
                private readonly IActionCard _card;

                public PlayCardEffect(IActionCard card)
                {
                    _card = card;
                }

                public override void Resolve(TurnContext context, ICard source)
                {
                    _card.Play(context);
                    _card.MoveTo(context.ActivePlayer.PlayArea);
                }
            }
        }
    }
}