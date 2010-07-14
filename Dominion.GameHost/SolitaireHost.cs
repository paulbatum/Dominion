using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;

namespace Dominion.GameHost
{
    public class SolitaireHost
    {        
        public Game CreateNewGame(string playerName)
        {
            var startingConfig = new SimpleStartingConfiguration(1);
            var bank = new CardBank();

            startingConfig.InitializeBank(bank);

            return new Game(new[] { new Player(playerName, startingConfig.CreateStartingDeck()) }, bank);
        }
        
    }
}
