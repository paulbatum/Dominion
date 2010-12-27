using System;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;

namespace Dominion.GameHost
{
    public class ChooseAPileMessage : IGameActionMessage
    {
        public ChooseAPileMessage(Guid playerId, Guid pileId)
        {
            PlayerId = playerId;
            PileId = pileId;
        }

        public Guid PlayerId { get; private set; }
        public Guid PileId { get; private set; }        

        public void UpdateGameState(Game game)
        {
            var player = game.Players.Single(p => p.Id == PlayerId);
            var pile = game.Bank.Piles.SingleOrDefault(p => p.Id == PileId);
            var activity = game.GetPendingActivity(player) as ISelectPileActivity;

            if (activity == null)
                throw new InvalidOperationException("There must be a corresponding activity");

            activity.SelectPile(pile);
        }

        public void Validate(Game game)
        {
         
        }
    }
}