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

        public MultiGameHost()
        {
            _clients = new Dictionary<Guid, IGameClient>();
            _gameData = new Dictionary<string, GameData>();
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
            var key = _gameData.Count.ToString();
            var startingConfig = new SimpleStartingConfiguration(numberOfPlayers);
            var game = startingConfig.CreateGame(playerNames);

            var host = new LockingGameHost(game);
            host.AcceptMessage(new BeginGameMessage());
            _gameData[key] = new GameData { GameKey = key };

            int aiPlayerCount = 0;
            foreach (var player in game.Players)
            {
                IGameClient client;
                if (AIClientConstructors.ContainsKey(player.Name))
                {
                    string newName = string.Format("{0} (Comp {1})", player.Name, ++aiPlayerCount);
                    client = AIClientConstructors[player.Name](player.Id, newName);
                    player.Rename(newName);
                }
                else
                {
                    client = new GameClient(player.Id, player.Name);
                }

                host.RegisterGameClient(client, player);
                _clients[player.Id] = client;
                _gameData[key].Slots[player.Id] = player.Name;
            }

            return key;
        }

        private IDictionary<string, Func<Guid, string, IGameClient>> mAIClientConstructors;
        private IDictionary<string, Func<Guid, string, IGameClient>> AIClientConstructors
        {
            get
            {
                if (mAIClientConstructors == null)
                {
                    mAIClientConstructors = new Dictionary<string, Func<Guid, string, IGameClient>>();

                    var aiTypeList = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(BaseAIClient)));
                    var expectedConstructorArguments = new Type[] { typeof(Guid), typeof(string) };
                    foreach (var aiType in aiTypeList)
                    {
                        var constructor = aiType.GetConstructor(expectedConstructorArguments);
                        Func<Guid, string, IGameClient> constructorCall = (id, name) =>
                            {
                                return (IGameClient)constructor.Invoke(new object[] { id, name });
                            };
                        mAIClientConstructors.Add(aiType.Name, constructorCall);
                    }
                }
                return mAIClientConstructors;
            }
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