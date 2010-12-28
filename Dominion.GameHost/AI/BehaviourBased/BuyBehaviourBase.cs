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
            var activityType = activity.ParseType();
            return activityType == ActivityType.DoBuys 
                || activityType == ActivityType.SelectPile;
        }

        public void Respond(IGameClient client, ActivityModel activity, GameViewModel state)
        {
            var pile = SelectPile(state, client);

            TalkSmack(pile, client);

            IGameActionMessage message = null;

            if (activity.ParseType() == ActivityType.DoBuys)
            {
                message = new BuyCardMessage(client.PlayerId, pile.Id);
            }
            else if (activity.ParseType() == ActivityType.SelectPile)
            {
                message = new ChooseAPileMessage(client.PlayerId, pile == null ? Guid.Empty : pile.Id);
            }

            client.AcceptMessage(message);
        }

        protected virtual void TalkSmack(CardPileViewModel pile, IGameClient client)
        {
            
        }

        protected IList<CardPileViewModel> GetValidBuys(GameViewModel state)
        {
            return state.Bank.Where(p => p.CanBuy).ToList();
        }

        protected abstract CardPileViewModel SelectPile(GameViewModel state, IGameClient client);
    }
}