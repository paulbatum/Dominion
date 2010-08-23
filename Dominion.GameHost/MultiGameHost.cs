using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.GameHost
{
    public class MultiGameHost
    {
        private IDictionary<string, IGameHost> _games;

        public MultiGameHost()
        {
            _games = new Dictionary<string, IGameHost>();
        }

        public string CreateNewGame()
        {
            var key = _games.Count.ToString();
            var host = new SolitaireHost();
            _games[key] = host;

            host.CreateNewGame("Player");
            return key;
        }

        public IGameHost FindGame(string key)
        {
            return _games[key];
        }

        public bool GameExists(string key)
        {
            return _games.ContainsKey(key);
        }

        public event Action<string> GameStateUpdated;

        private readonly object _gameStateLock = new object();
        
        public void RaiseGameStateUpdated(string key)
        {
            lock (_gameStateLock)
            {
                if (GameStateUpdated != null)
                    GameStateUpdated(key);

                GameStateUpdated = null;
            }
        }
    }
}