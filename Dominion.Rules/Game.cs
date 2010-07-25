using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules
{
    public class Game
    {
        private IList<Player> _players;
        public Player ActivePlayer { get; private set; }
        public CardBank Bank { get; private set; }        

        public Game(IEnumerable<Player> players, CardBank bank)
        {
            if(!players.Any())
                throw new ArgumentException("There must be at least one player");

            _players = new List<Player>(players);
            Bank = bank;
        }

        public IEnumerable<Player> Players
        {
            get { return _players; }
        }

        private IEnumerable<Player> TurnLoop
        {
            get
            {
                int current = 0;
                while(true)
                {
                    yield return _players[current++];
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

                ActivePlayer = player;
                yield return player.BeginTurn();
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