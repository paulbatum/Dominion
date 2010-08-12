using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;

namespace Dominion.GameHost
{
    public interface IGameHost
    {
        Game CurrentGame { get; set; }
        TurnContext CurrentTurn { get; set; }
        bool NextTurn();
        void BeginNewGame();
    }

    public class SolitaireHost : IGameHost
    {
        private IEnumerator<TurnContext> _turns;

        public Game CreateNewGame(string playerName)
        {
            var startingConfig = new SimpleStartingConfiguration(1);
            var bank = new CardBank();

            startingConfig.InitializeBank(bank);

            this.CurrentGame = new Game(new[] { new Player(playerName, startingConfig.CreateStartingDeck()) }, bank);
            
            _turns = CurrentGame.GameTurns().GetEnumerator();
            NextTurn();
            
            return this.CurrentGame;
        }

        public bool NextTurn()
        {
            if(CurrentTurn != null)
                CurrentTurn.EndTurn();

            if (_turns.MoveNext() == false)
                return false;

            CurrentTurn = _turns.Current;
            return true;
        }

        public Game CurrentGame { get; set; }
        public TurnContext CurrentTurn { get; set; }

        public void BeginNewGame()
        {
            CreateNewGame("Player");
        }
    }
}
