using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;

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

        public string CreateNewGame(IEnumerable<string> playerNames, int numberOfPlayers)
        {
            var key = _games.Count.ToString();
            var startingConfig = new SimpleStartingConfiguration(numberOfPlayers);
            var game = startingConfig.CreateGame(playerNames);

            var host = new LockingGameHost(game);
            host.AcceptMessage(new BeginGameMessage());
            _games[key] = host;
            _gameData[key] = new GameData { GameKey = key };

            foreach (var player in game.Players)
            {
                var client = new GameClient(player.Id, player.Name);
                host.RegisterGameClient(client, player);
                _clients[player.Id] = client;
                _gameData[key].Slots[player.Id] = player.Name;
            }

            return key;
        }
    }

    public class GameData
    {
        public string GameKey { get; set; }
        public IDictionary<Guid, string> Slots { get; private set; }

        public GameData()
        {
            Slots = new Dictionary<Guid, string>();
        }
    }
}