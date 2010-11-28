using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.Activities;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public abstract class BuyBehaviourBase : IAIBehaviour
    {
        public virtual bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return activity.ParseType() == ActivityType.DoBuys;
        }

        public void Respond(IGameClient client, ActivityModel activity, GameViewModel state)
        {
            var pile = SelectPile(state);

            TalkSmack(pile, client);
            var message = new BuyCardMessage(client.PlayerId, pile.Id);            
            client.AcceptMessage(message);
        }

        protected virtual void TalkSmack(CardPileViewModel pile, IGameClient client)
        {
            
        }

        protected IList<CardPileViewModel> GetValidBuys(GameViewModel state)
        {
            return state.Bank.Where(p => p.CanBuy).ToList();
        }

        protected abstract CardPileViewModel SelectPile(GameViewModel state);
    }
}