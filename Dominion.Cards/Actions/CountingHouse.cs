using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Cards.Treasure;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class CountingHouse : Card, IActionCard
    {
        public CountingHouse() : base(5)
        {
        }

        public void Play(TurnContext context)
        {
            var coppers = context.ActivePlayer.Discards.OfType<Copper>().ToList();

            if (coppers.Count > 0)
            {
                context.Game.Log.LogMessage("{0} put {1} Copper from their discard pile into their hand",
                                            context.ActivePlayer.Name, coppers.Count);

                foreach (var copper in coppers)
                    copper.MoveTo(context.ActivePlayer.Hand);
            }
            else
            {
                context.Game.Log.LogMessage("Counting house did nothing - no Copper in discard pile.");
            }

        }
    }
}
