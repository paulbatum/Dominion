using System;
using System.Linq;
using System.Collections.Generic;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules.Activities
{
    public class GainACardActivity : ActivityBase, IRestrictedActivity
    {
        public GainACardActivity(IGameLog log, Player player, string message, ActivityType type) 
            : base(log, player, message, type)
        {
            Restrictions = new List<RestrictionType>();
            GetDestinationPile = p => p.Discards;
        }

        /// <summary>
        /// If empty, no restrictions. Otherwise, card must be of a type contained in this list.
        /// </summary>
        public IList<RestrictionType> Restrictions { get; private set; }

        public virtual void SelectPileToGainFrom(CardPile pile)
        {
            var card = pile.TopCard;
            this.EnsureCardIsAllowed(card);

            var destination = GetDestinationPile(Player);
            card.MoveTo(destination);
            Log.LogGain(Player, card);
            IsSatisfied = true;
        }

        public Func<Player, CardZone> GetDestinationPile { get; set; }
    }
}