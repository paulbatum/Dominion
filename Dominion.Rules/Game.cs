using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.Activities;
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
        public long Version { get; private set; }
        public TrashPile Trash {get; private set; }

        public Game(IEnumerable<Player> players, CardBank bank, IGameLog log)
        {
            if(!players.Any())
                throw new ArgumentException("There must be at least one player");

            _players = new List<Player>(players);

            Bank = bank;
            Log = log;
            Trash = new TrashPile();

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

        public Player PlayerToLeftOf(Player player)
        {
            if (player == _players.Last())
                return _players.First();

            return _players.SkipWhile(p => p != player).Skip(1).First();
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
                _activePlayer = player;
                yield return player.BeginTurn(this);
            }                
        }

        public TurnContext CurrentTurn
        {
            get
            {                
                return _gameTurns.Current;
            }
        }


        public bool IsComplete { get; private set; }

        protected bool GameEndingPileDepleted
        {
            get { return Bank.EmptyGameEndingPilesCount > 0; }
        }

        private bool TooManyEmptyPiles
        {
            get { return Bank.EmptyPileCount == (_players.Count < 5 ? 3 : 4); }
        }

        private void CheckGameComplete()
        {
            IsComplete = TooManyEmptyPiles || GameEndingPileDepleted;
        }

        public GameScores Score()
        {
            var scores = new GameScores(this);

            foreach(var player in Players)
                scores.Score(player);                

            return scores;
        }

        public void EndTurn()
        {
            CurrentTurn.EndTurn();      
            CheckGameComplete();

            if (IsComplete)
                Log.LogGameEnd(this);
            else
                _gameTurns.MoveNext();
        }

        public IActivity GetPendingActivity(Player player)
        {
            var currentEffect = CurrentTurn.GetCurrentEffect();
            if (currentEffect != null)
                return currentEffect.GetActivity(player);

            if (player == ActivePlayer)
            {
                return CurrentTurn.GetDefaultActivity();
            }
            else
            {
                return new WaitingForPlayersActivity(player);
            }            
        }

        public void IncrementVersion()
        {
            Version++;
        }
    }

}
