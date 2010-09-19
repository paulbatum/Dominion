using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.CardTypes;
using Dominion.Cards.Curses;

namespace Dominion.Cards.Actions
{
    public class Witch : Card, IActionCard
    {
        public Witch()
           : base(5)
        {

        }

        public void Play(TurnContext context)
        {
            context.DrawCards(2);

            foreach (var player in context.Opponents)
            {
                if (player.Hand.OfType<Moat>().Any())
                {
                    context.Game.Log.LogMoat(player);
                    continue;
                }

                var cursePile = context.Game.Bank.Piles.SingleOrDefault(x => x.TopCard is Curse);

                if (cursePile != null)
                {
                    var card = cursePile.TopCard;
                    card.MoveTo(player.Discards);
                    context.Game.Log.LogGain(player, card);
                }
                else
                {
                    context.Game.Log.LogMessage("{0} avoided a Curse because the pile is empty", player.Name);
                }
            }
        }
    }
}
