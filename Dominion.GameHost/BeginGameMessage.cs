using System;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.GameHost
{
    public class BeginGameMessage : IGameActionMessage
    {
        public void UpdateGameState(Game game)
        {
            TurnContext tempQualifier = game.CurrentTurn;
            if (tempQualifier.ActivePlayer.Hand.OfType<IActionCard>().Any() == false || tempQualifier.RemainingActions == 0)
                tempQualifier.MoveToBuyStep();
        }

        public void Validate(Game game)
        {
            
        }
    }

}