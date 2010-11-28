using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.GameHost.AI
{
    public class OldBigMoneyAI : SimpleAI
    {

        protected override void DiscardCards(int count, GameViewModel currentState)
        {
            var discardPreference = new List<string> { "Curse", "Estate", "Duchy", "Province", "Colony", "Copper", "Silver", "Gold", "Platinum" };
            var orderedHand = currentState.Hand                
                .OrderBy(c => discardPreference.IndexOf(c.Name)).ToList();
            var cardIdsToDiscard = orderedHand.Take(count).Select(c => c.Id).ToArray();
            var discardAction = new SelectCardsMessage(_client.PlayerId, cardIdsToDiscard.ToArray());
            _client.AcceptMessage(discardAction);
        }

        protected virtual IList<string> GetPriorities(GameViewModel state)
        {
            var priorities = new List<string> {"Colony", "Platinum", "Province", "Gold", "Silver", "Copper"};
            if(state.Bank.Single(p => p.Name == "Province").Count < 7)
                priorities.Insert(3, "Duchy");

            return priorities;
        }

        protected override IGameActionMessage DoTurn(GameViewModel state)
        {
            //Only buy
            if (!state.Status.InBuyStep)
                return new MoveToBuyStepMessage(_client.PlayerId);

            var validBuys = GetValidBuys(state);
            var priorities = GetPriorities(state);


            CardPileViewModel pile = validBuys
                .Where(x => priorities.Contains(x.Name))
                .OrderBy(x => priorities.IndexOf(x.Name))
                .First();

            if(pile.Name == "Province")
                _client.SendChatMessage("Province muthafucka!");

            if(pile.Name == "Colony")
                _client.SendChatMessage("COLONY! SUCK IT!");

            return new BuyCardMessage(_client.PlayerId, pile.Id);
        }

    }
}
