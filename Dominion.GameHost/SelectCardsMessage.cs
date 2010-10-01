using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;

namespace Dominion.GameHost
{
    public class SelectCardsMessage : IGameActionMessage
    {
        public SelectCardsMessage(Guid playerId, Guid[] cardIds)
        {
            PlayerId = playerId;
            CardIds = cardIds ?? new Guid[]{};
        }

        public Guid PlayerId { get; private set; }
        public Guid[] CardIds { get; private set; }

        public void UpdateGameState(Game game)
        {
            var player = game.Players.Single(p => p.Id == PlayerId);
            var activity = game.GetPendingActivity(player) as ISelectCardsActivity;

            if (activity == null)
                throw new InvalidOperationException("There must be a corresponding activity");

            IEnumerable<ICard> cardSource;

            if(activity is ISelectFromRevealedCardsActivity)
            {
                cardSource = ((ISelectFromRevealedCardsActivity) activity).RevealedCards;
            }
            else
            {
                cardSource = player.Hand;
            }

            var cards = cardSource.Where(c => CardIds.Contains(c.Id)).ToList();                   
            activity.SelectCards(cards);            
        }

        public void Validate(Game game)
        {
            
        }
    }
}