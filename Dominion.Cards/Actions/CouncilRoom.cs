using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class CouncilRoom : Card, IActionCard
    {
        public CouncilRoom()
            : base(5)
        {

        }

        public void Play(TurnContext context)
        {
            context.DrawCards(4);
            context.Buys += 1;

            foreach (var player in context.Game.Players)
            {
                if (player != context.ActivePlayer)
                {
                    player.DrawCards(1);
                }
            }
        }
    }
}
