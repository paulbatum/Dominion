using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.GameHost.AI
{
    public class BigMoneyAI : BaseAIClient
    {
        public BigMoneyAI(Guid playerId, string playerName)
            : base (playerId, playerName)
        {
        }

        protected override void DiscardCards(int count, GameViewModel currentState)
        {
            var discardPreference = new List<string> { "Estate", "Duchy", "Province", "Curse", "Copper", "Silver", "Gold" };
            var orderedHand = currentState.Hand.OrderBy(c => discardPreference.IndexOf(c.Name)).ToList();
            var cardIdsToDiscard = orderedHand.Take(count).Select(c => c.Id).ToArray();
            var discardAction = new SelectCardsFromHandMessage(PlayerId, cardIdsToDiscard.ToArray());
            AcceptMessage(discardAction);
        }

        protected override void DoTurn(GameViewModel state)
        {
            //Only buy
            if (!state.Status.InBuyStep)
            {
                var doBuysMessage = new MoveToBuyStepMessage(PlayerId);
                AcceptMessage(doBuysMessage);
                return;
            }

            Guid pileIdToBuyFrom = Guid.Empty;
            if (state.Status.MoneyToSpend >= 8)
                pileIdToBuyFrom = state.Bank.Single(p => p.Name == "Province").Id;
            else if (state.Status.MoneyToSpend >= 6)
                pileIdToBuyFrom = state.Bank.Single(p => p.Name == "Gold").Id;
            else if (state.Status.MoneyToSpend >= 3)
                pileIdToBuyFrom = state.Bank.Single(p => p.Name == "Silver").Id;
            else
                pileIdToBuyFrom = state.Bank.Single(p => p.Name == "Copper").Id;

            var buyMessage = new BuyCardMessage(PlayerId, pileIdToBuyFrom);
            AcceptMessage(buyMessage);
        }
    }
}
