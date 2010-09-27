using System;
using System.Collections.Generic;

namespace Dominion.Rules.Activities
{
    public class GainACardUpToActivity : GainACardActivity
    {
        public GainACardUpToActivity(IGameLog log, Player player, int upToCost) 
            : base(log, player, string.Format("Select a card to gain of cost {0} or less", upToCost), ActivityType.SelectPile)
        {
            UpToCost = upToCost;
        }

        public int UpToCost { get; private set; }

        public override void SelectPileToGainFrom(CardPile pile)
        {
            if(pile.TopCard.Cost > UpToCost)
                throw new ArgumentException("Pile cost exceeds limit.", "pile");

            base.SelectPileToGainFrom(pile);
        }

        public override IDictionary<string, object> Properties
        {
            get
            {
                var properties = base.Properties;
                properties["UpToCost"] = UpToCost;
                return properties;
            }
        }
    }
}