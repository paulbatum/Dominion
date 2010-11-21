using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class GhostShip : Card, IActionCard, IAttackCard
    {
        public GhostShip()
            : base(5)
        {
        }

        public void Play(TurnContext context)
        {
            context.ActivePlayer.DrawCards(2);
            context.AddEffect(new GhostShipAttack());
        }

        private class GhostShipAttack : AttackEffect
        {
            public override void Attack(Player player, TurnContext context)
            {
                int cardsToPutBack = player.Hand.CardCount - 3;

                var activities = Activities.PutMultipleCardsFromHandOnTopOfDeck(context.Game.Log, player, cardsToPutBack);
                foreach(var activity in activities)
                    _activities.Add(activity);
            }
        }
    }
}
