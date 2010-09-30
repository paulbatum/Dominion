using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPassiveCardEffect
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentCost">The current cost of the card, potentially already modified</param>
        /// <param name="card">The card whose cost is being modified</param>
        /// <returns></returns>
        int ModifyCost(int currentCost, ICard card);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentValue">The current value of the card, potentially already modified</param>
        /// <param name="card">The card whose value is being modified</param>
        /// <returns></returns>
        int ModifyValue(int currentValue, ITreasureCard card);
    }

    public class PassiveCardEffectBase : IPassiveCardEffect
    {
        public virtual int ModifyCost(int currentCost, ICard card)
        {
            return currentCost;
        }

        public virtual int ModifyValue(int currentValue, ITreasureCard card)
        {
            return currentValue;
        }
    }
}
