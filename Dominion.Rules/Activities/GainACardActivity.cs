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
        }

        /// <summary>
        /// If empty, no restrictions. Otherwise, card must be of a type contained in this list.
        /// </summary>
        public IList<RestrictionType> Restrictions { get; private set; }

        public virtual void SelectPileToGainFrom(CardPile pile)
        {
            var card = pile.TopCard;
            this.EnsureCardIsAllowed(card);

            card.MoveTo(Player.Discards);
            Log.LogGain(Player, card);
            IsSatisfied = true;
        }
    }
}