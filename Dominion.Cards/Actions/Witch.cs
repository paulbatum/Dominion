using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Witch : ActionCard
    {
        public Witch()
           : base(5)
        {

        }

        protected override void Play(TurnContext context)
        {
            context.DrawCards(2);

            foreach (var player in context.Opponents)
            {
                if (player.Hand.OfType<Moat>().Any())
                {
                    context.Game.Log.LogMoat(player);
                    continue;
                }

                var card = context.Game.Bank.Piles.Single(x => x.TopCard is CurseCard).TopCard;
                card.MoveTo(player.Discards);
                context.Game.Log.LogGain(player, card);
            }
        }
    }
}
