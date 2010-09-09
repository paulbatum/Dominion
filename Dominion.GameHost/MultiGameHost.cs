using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.GameHost
{
    public class MultiGameHost
    {
        private readonly IDictionary<string, IGameHost> _games;
        private readonly IDictionary<Guid, IGameClient> _clients;
        private readonly IDictionary<string, GameData> _gameData;

        public MultiGameHost()
        {
            _games = new Dictionary<string, IGameHost>();
            _clients = new Dictionary<Guid, IGameClient>();
            _gameData = new Dictionary<string, GameData>();
        }

        public string CreateNewGame()
        {
            var key = _games.Count.ToString();
            var startingConfig = new SimpleStartingConfiguration(2);
            var game = startingConfig.CreateGame(new[] {"Player1", "Player2"});
            
            var host = new LockingGameHost(game);
            host.AcceptMessage(new BeginGameMessage());
            _games[key] = host;
            _gameData[key] = new GameData {GameKey = key};

            foreach(var player in game.Players)
            {
                var client = new GameClient(player.Id, player.Name);
                host.RegisterGameClient(client, player);
                _clients[player.Id] = client;
                _gameData[key].Slots[player.Name] = player.Id;
            }

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

        public IGameClient FindClient(Guid id)
        {
            return _clients[id];
        }

        public GameData GetGameData(string key)
        {
            return _gameData[key];
        }
    }

    public class GameData
    {
        public string GameKey { get; set; }
        public IDictionary<string, Guid> Slots { get; private set; }

        public GameData()
        {
            Slots = new Dictionary<string, Guid>();
        }
    }
}