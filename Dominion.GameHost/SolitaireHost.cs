using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;

namespace Dominion.GameHost
{
    public interface IGameHost
    {
        Game CurrentGame { get; }
        void BeginNewGame();
    }

    public class SolitaireHost : IGameHost
    {
        public Game CreateNewGame(string playerName)
        {
            var startingConfig = new SimpleStartingConfiguration(1);
            var bank = new CardBank();

            startingConfig.InitializeBank(bank);

            this.CurrentGame = new Game(new[] { new Player(playerName, startingConfig.CreateStartingDeck()) }, bank);
            return this.CurrentGame;
        }

        public Game CurrentGame { get; set; }

        public void BeginNewGame()
        {
            CreateNewGame("Player");
        }
    }
}
