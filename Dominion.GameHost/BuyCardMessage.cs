using System;
using System.Linq;
using Dominion.Rules;

namespace Dominion.GameHost
{
    public class BuyCardMessage : IGameActionMessage
    {
        public BuyCardMessage(Guid playerId, Guid pileId)
        {
            PlayerId = playerId;
            PileId = pileId;
        }

        public Guid PlayerId { get; private set; }
        public Guid PileId { get; private set; }        

        public void UpdateGameState(Game game)
        {            
            CardPile pile = game.Bank.Piles.Single(p => p.Id == PileId);
            game.CurrentTurn.Buy(pile);            
        }

        public void Validate(Game game)
        {
            if(game.ActivePlayer.Id != PlayerId)
                throw new InvalidOperationException(string.Format("Player '{0}' is not active.", PlayerId));
        }
    }
}