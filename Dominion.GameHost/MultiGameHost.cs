using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.GameHost.AI;
using System.Reflection;

namespace Dominion.GameHost
{
    public class MultiGameHost
    {
        private readonly IDictionary<Guid, IGameClient> _clients;
        private readonly IDictionary<string, GameData> _gameData;
        private readonly IList<Type> _aiTypeList;
        
        public MultiGameHost()
        {
            _clients = new Dictionary<Guid, IGameClient>();
            _gameData = new Dictionary<string, GameData>();
            _aiTypeList =
                Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof (BaseAIClient))).ToList();
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
            var someCards = new List<string>{"Smithy", "Moat", "Witch", "Market", "SeaHag", "Adventurer", "Militia", "Village", "Caravan", "CouncilRoom"};

            return CreateNewGame(playerNames, numberOfPlayers, someCards, false);
        }

        public string CreateNewGame(IEnumerable<string> playerNames, int numberOfPlayers, IEnumerable<string> selectedCardNames, bool useProsperity)
        {
            var key = _gameData.Count.ToString();
            var startingConfig = new ChosenStartingConfiguration(numberOfPlayers, selectedCardNames, useProsperity);
            var game = startingConfig.CreateGame(playerNames);

            var host = new LockingGameHost(game);            
            _gameData[key] = new GameData { GameKey = key };

            int aiPlayerCount = 0;
            foreach (var player in game.Players)
            {
                IGameClient client = new GameClient(player.Id, player.Name);

                if (_aiTypeList.Select(x => x.Name).Contains(player.Name))
                {                    
                    var ai = (BaseAIClient) Activator.CreateInstance(_aiTypeList.Single(x => x.Name == player.Name));
                    ai.Attach(client);
                    string newName = string.Format("{0} (Comp {1})", player.Name, ++aiPlayerCount);
                    player.Rename(newName);
                }

                host.RegisterGameClient(client, player);
                _clients[player.Id] = client;
                _gameData[key].Slots[player.Id] = player.Name;
            }

            host.AcceptMessage(new BeginGameMessage());

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