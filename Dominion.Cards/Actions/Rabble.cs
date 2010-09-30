using System;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    //public class Rabble : Card, IActionCard, IAttackCard
    //{
    //    public Rabble() : base(5)
    //    {
    //    }

    //    public void Play(TurnContext context)
    //    {
    //        context.DrawCards(3);
    //        context.AddEffect(new RabbleAttack());
    //    }

    //    private class RabbleAttack : AttackEffect
    //    {
    //        public override void Attack(Player player, TurnContext context)
    //        {
    //            var revealZone = new RevealZone(player);
    //            player.Deck.MoveTop(3, revealZone);

    //            revealZone.LogReveal(context.Game.Log);
    //            foreach(var card in revealZone.Where(c => c is IActionCard || c is ITreasureCard))
    //                card.MoveTo(player.Discards);

    //            if (revealZone.Count() == 1)
    //                player.Deck.MoveToTop(revealZone.Single());
    //            else if (revealZone.Count() > 1)
    //            {
    //                foreach (var activity in Activities.PutRevealedCardsOnTopOfDeck(context.Game.Log, player, revealZone))
    //                    _activities.Add(activity);
    //            }

    //        }


    //    }
    //}
}