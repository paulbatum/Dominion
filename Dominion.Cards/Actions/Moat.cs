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

        public void React(AttackEffect attackEffect, Player player, IGameLog log)
        {
            attackEffect.Nullify(player);
            log.LogMessage("{0} is protected due to a moat in hand.", player.Name);
        }
    }
}