using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.GameHost.AI
{
    public class BigMoneyAI : BaseAIClient
    {

        protected override void DiscardCards(int count, GameViewModel currentState)
        {
            var discardPreference = new List<string> { "Estate", "Duchy", "Province", "Curse", "Copper", "Silver", "Gold" };
            var orderedHand = currentState.Hand                
                .OrderBy(c => discardPreference.IndexOf(c.Name)).ToList();
            var cardIdsToDiscard = orderedHand.Take(count).Select(c => c.Id).ToArray();
            var discardAction = new SelectCardsMessage(_client.PlayerId, cardIdsToDiscard.ToArray());
            _client.AcceptMessage(discardAction);
        }

        protected virtual IList<string> GetPriorities(GameViewModel state)
        {
            var priorities = new List<string> {"Province", "Gold", "Silver", "Copper"};
            if(state.Bank.Single(p => p.Name == "Province").Count < 7)
                priorities.Insert(1, "Duchy");

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

            return new BuyCardMessage(_client.PlayerId, pile.Id);
        }

    }
}
