using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules
{
    public class Game
    {
        private IList<Player> _players;
        private IEnumerator<TurnContext> _gameTurns;
        private Player _activePlayer;
        public CardBank Bank { get; private set; }
        public IGameLog Log { get; private set; }

        public Game(IEnumerable<Player> players, CardBank bank, IGameLog log)
        {
            if(!players.Any())
                throw new ArgumentException("There must be at least one player");

            _players = new List<Player>(players);

            Bank = bank;
            Log = log;

            _gameTurns = GameTurns().GetEnumerator();
            _gameTurns.MoveNext();
        }

        public IEnumerable<Player> Players
        {
            get { return _players; }
        }

        public Player ActivePlayer
        {
            get { return CurrentTurn.ActivePlayer; }
        }

        private IEnumerable<Player> TurnLoop
        {
            get
            {
                int current = 0;
                while(true)
                {
                    var nextPlayer = _players[current++];
                    Log.LogTurn(nextPlayer);
                    yield return nextPlayer;
                    if (current == _players.Count)
                        current = 0;
                }
            }
        }

        public IEnumerable<TurnContext> GameTurns()
        {
            foreach (var player in TurnLoop)
            {
                if(IsComplete)
                {                    
                    yield break;
                }                    

                _activePlayer = player;
                yield return player.BeginTurn(this);
            }                
        }

        public TurnContext CurrentTurn
        {
            get
            {
                if(_gameTurns.Current.HasEnded)
                    _gameTurns.MoveNext();

                return _gameTurns.Current;
            }
        }
        

        public bool IsComplete
        {
            get { return TooManyEmptyPiles || GameEndingPileDepleted; }
        }

        protected bool GameEndingPileDepleted
        {
            get { return Bank.EmptyGameEndingPilesCount > 0; }
        }

        private bool TooManyEmptyPiles
        {
            get { return Bank.EmptyPileCount == (_players.Count < 5 ? 3 : 4); }
        }
    }

}