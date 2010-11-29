using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dominion.GameHost;
using Dominion.GameHost.AI;
using Dominion.GameHost.AI.BehaviourBased;

namespace Dominion.AIWorkbench
{
    public class Simulation
    {
        public List<string> Cards { get; set; }
        public Dictionary<string, Type> Players { get; set; }
        public int NumberOfGamesToExecute { get; set; }

        private List<GameResultsViewModel> _results;

        public Simulation()
        {
            NumberOfGamesToExecute = 1000;
        }

        public void Run(Action<Task<ResultsSummary>> onUpdateResults)
        {
            _results = new List<GameResultsViewModel>();
            var startingConfig = new ChosenStartingConfiguration(Players.Count, Cards, false);

            TaskScheduler syncContext = TaskScheduler.FromCurrentSynchronizationContext();

            //Task<GameResultsViewModel>[] gameResults = new Task<GameResultsViewModel>[NumberOfGamesToExecute];

            for (int i = 0; i < NumberOfGamesToExecute; i++)
            {
                Task.Factory.StartNew(() => RunGame(startingConfig))
                    .ContinueWith(t => CreateSummary(t.Result))
                    .ContinueWith(onUpdateResults, syncContext);
            }

            
            

            //Parallel.For(0, NumberOfGamesToExecute, i =>
            //    {
            //        var result = RunGame(startingConfig);
            //        _results.Add(result);
            //        onUpdateResults(CreateSummary());
            //    });
            
            
            //onUpdateResults(CreateSummary());
        }



        private GameResultsViewModel RunGame(ChosenStartingConfiguration startingConfig)
        {
            var game = startingConfig.CreateGame(Players.Keys);
            var clients = new List<IGameClient>();

            var host = new LockingGameHost(game);

            foreach (var player in game.Players)
            {
                IGameClient client = new GameClient(player.Id, player.Name);
                clients.Add(client);

                var ai = (BaseAIClient) Activator.CreateInstance(Players[player.Name]);
                ai.Attach(client);
                
                host.RegisterGameClient(client, player);
            }

            host.AcceptMessage(new BeginGameMessage());

            var firstClient = clients.First();

            while(!firstClient.GetGameState().Status.GameIsComplete)
                Thread.Sleep(500);

            return firstClient.GetGameState().Results;
        }

        private ResultsSummary CreateSummary(GameResultsViewModel result)
        {
            lock (_results)
            {
                _results.Add(result);

                var summary = new ResultsSummary();
                foreach (var kvp in Players)
                {
                    string player = kvp.Key;
                    var winPercentage = ((decimal) _results.Count(x => x.Winner == player) / _results.Count()) * 100.0m;
                    var totalScore = _results.Sum(x => x.Scores.Single(p => p.PlayerName == player).Score);
                    summary.AddResult(player, winPercentage, totalScore);
                    summary.CompletedGameCount = _results.Count();
                }

                return summary;
            }
        }

    }

    public class ResultsSummary
    {
        public int CompletedGameCount { get; set; }
        public IList<PlayerResults> Results { get; set; }

        public ResultsSummary()
        {
            Results = new List<PlayerResults>();
        }

        public void AddResult(string playerName, decimal winPercentage, long totalScore)
        {
            Results.Add(new PlayerResults { PlayerName = playerName, WinPercentage = winPercentage, TotalScore = totalScore});
        }

        public class PlayerResults
        {
            public string PlayerName { get; set; }
            public decimal WinPercentage { get; set; }
            public long TotalScore { get; set; }
        }
    }
}