using System;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Moat : Card, IActionCard, IReactionCard
    {
        public Moat() : base(2)
        {
        }

        public void Play(TurnContext context)
        {
            context.DrawCards(2);
        }

        public void React(AttackEffect attackEffect, Player player, TurnContext currentTurn)
        {
            attackEffect.Nullify(player);
            currentTurn.Game.Log.LogMessage("{0} reveals a moat and nullifies the attack.", player.Name);
        }
    }
}