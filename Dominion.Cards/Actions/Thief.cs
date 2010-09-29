using System;
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
            context.AddEffect(new ThiefAttack());
        }

        private class ThiefAttack : AttackEffect
        {
            public override void Attack(Player player, TurnContext context)
            {
                var log = context.Game.Log;
                var revealZone = new RevealZone(player);

                // Top 2
                if(player.Deck.TopCard != null)
                    player.Deck.TopCard.MoveTo(revealZone);
                if(player.Deck.TopCard != null)
                    player.Deck.TopCard.MoveTo(revealZone);

                log.LogMessage("{0} revealed {1}.", player.Name, revealZone);

                var revealedTreasures = revealZone.OfType<ITreasureCard>().WithDistinctTypes();

                switch(revealedTreasures.Count())
                {
                    case 0:
                        revealZone.MoveAll(player.Discards);
                        return;
                    case 1:
                        var trashedCard = revealedTreasures.Single();
                        trashedCard.MoveTo(context.Game.Trash);
                        log.LogTrash(player, trashedCard);
                        revealZone.MoveAll(player.Discards);
                        var gainOrTrashActivity = CreateGainOrTrashActivity(context, trashedCard, TODO);
                        _activities.Add(gainOrTrashActivity);
                        break;
                    default:
                        var chooseTreasureActivity = CreateChooseTreasureActivity(context, revealZone);
                        _activities.Add(chooseTreasureActivity);
                        break;
                }

            }

            private IActivity CreateChooseTreasureActivity(TurnContext context, RevealZone revealZone)
            {
                var selectTreasure = new SelectFromRevealedCardsActivity(context, revealZone, 
                    "Select a treasure to trash (you will have the opportunity to gain it).", SelectionSpecifications.SelectExactlyXCards(1));
                
                selectTreasure.AfterCardsSelected = cards =>
                {
                    var card = cards.OfType<ITreasureCard>().Single();
                    card.MoveTo(context.Game.Trash);
                    revealZone.MoveAll(revealZone.Owner.Discards);

                    var gainOrTrashActivity = CreateGainOrTrashActivity(context, card, revealZone.Owner);
                    _activities.Insert(0, gainOrTrashActivity);
                };
                
                return selectTreasure;
            }

            private IActivity CreateGainOrTrashActivity(TurnContext context, ITreasureCard trashedCard, Player owner)
            {                
                var gainOrTrash = new ChoiceActivity(context, context.ActivePlayer, 
                                                     string.Format("Gain {0}'s {1}?", owner.Name, trashedCard),Choice.Yes, Choice.No);
                
                gainOrTrash.ActOnChoice = choice =>
                {
                    if(choice == Choice.Yes)
                    {
                        trashedCard.MoveTo(context.ActivePlayer.Discards);
                        context.Game.Log.LogGain(context.ActivePlayer, (Card)trashedCard);
                    }
                };

                return gainOrTrash;
            }
        }

      
    }    
}