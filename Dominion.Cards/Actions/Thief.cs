using System.Linq;
using System.Collections.Generic;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Thief : Card, IActionCard, IAttackCard
    {
        public Thief() : base(4)
        {
        }

        public void Play(TurnContext context)
        {
            context.AddEffect(this, new ThiefAttack());
        }

        private class ThiefAttack : AttackEffect
        {
            public override void Attack(Player player, TurnContext context, ICard source)
            {                
                var revealZone = new RevealZone(player);
                player.Deck.MoveTop(2, revealZone);
                revealZone.LogReveal(context.Game.Log);                

                var revealedTreasures = revealZone.OfType<ITreasureCard>().WithDistinctTypes();

                switch(revealedTreasures.Count())
                {
                    case 0:
                        revealZone.MoveAll(player.Discards);
                        return;
                    case 1:
                        var trashedCard = TrashAndDiscard(context, revealZone, revealedTreasures);
                        var gainChoiceActivity = Activities.GainOpponentsCardChoice(context, trashedCard, revealZone.Owner, source);
                        _activities.Add(gainChoiceActivity);
                        break;
                    default:
                        var chooseTreasureActivity = CreateChooseTreasureActivity(context, revealZone, source);
                        _activities.Add(chooseTreasureActivity);
                        break;
                }
            }

            private Card TrashAndDiscard(TurnContext context, RevealZone revealZone, IEnumerable<ITreasureCard> revealedTreasures)
            {
                var trashedCard = revealedTreasures.Single();
                trashedCard.MoveTo(context.Game.Trash);
                context.Game.Log.LogTrash(revealZone.Owner, trashedCard);
                revealZone.MoveAll(revealZone.Owner.Discards);
                return (Card) trashedCard;
            }

            private IActivity CreateChooseTreasureActivity(TurnContext context, RevealZone revealZone, ICard source)
            {
                var selectTreasure = new SelectFromRevealedCardsActivity(context.Game.Log, context.ActivePlayer, revealZone,
                    "Select a treasure to trash (you will have the opportunity to gain it).", SelectionSpecifications.SelectExactlyXCards(1), source);
                
                selectTreasure.AfterCardsSelected = cards =>
                {
                    var trashedCard = TrashAndDiscard(context, revealZone, cards.OfType<ITreasureCard>());
                    var gainOrTrashActivity = Activities.GainOpponentsCardChoice(context, trashedCard, revealZone.Owner, source);
                    _activities.Insert(0, gainOrTrashActivity);
                };
                
                return selectTreasure;
            }            
        }
    }
}