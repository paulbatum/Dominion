using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        public string Name { get; set; }
        private GameResultsViewModel[] _results;

        public Simulation()
        {
            NumberOfGamesToExecute = 1000;
        }

        public void Run(Action<Task<ResultsSummary>> onUpdateResults, Action<Task> onDone)
        {
            _results = new GameResultsViewModel[NumberOfGamesToExecute];
            var startingConfig = new ChosenStartingConfiguration(Players.Count, Cards, false);

            TaskScheduler syncContext = TaskScheduler.FromCurrentSynchronizationContext();

            Task[] tasks = new Task[NumberOfGamesToExecute];

            for (int i = 0; i < NumberOfGamesToExecute; i++)
            {
                int temp = i;
                tasks[i] = Task.Factory.StartNew(() => RunGame(temp, startingConfig))
                    .ContinueWith(t => CreateSummary())
                    .ContinueWith(onUpdateResults, syncContext);
            }

            Task.Factory
                .ContinueWhenAll(tasks, WriteResults)
                .ContinueWith(onDone, syncContext);
        }

        private void WriteResults(Task[] obj)
        {
            var resultsBuilder = new StringBuilder();

            var playerNames = string.Join(", ", Players.Keys.ToArray());
            resultsBuilder.AppendLine("Game Number, " + playerNames);
            for(int i = 0; i < _results.Length; i++)
            {
                var result = _results[i];
                var scores = string.Join(", ", result.Scores.Select(s => s.Score.ToString()));
                resultsBuilder.AppendFormat("{0}, {1}", i, scores)
                    .AppendLine();
            }

            var output = resultsBuilder.ToString();

            File.WriteAllText(Path.Combine(Name, "scores.csv"), output);

            WriteSummaryToFile(CreateSummary());
        }


        private void RunGame(int gameNumber, ChosenStartingConfiguration startingConfig)
        {
            var game = startingConfig.CreateGame(Players.Keys);
            var clients = new List<IGameClient>();

            var host = new LockingGameHost(game);

            foreach (var player in game.Players)
            {
                IGameClient client = new GameClient(player.Id, player.Name);
                clients.Add(client);

                var ai = (BaseAIClient)Activator.CreateInstance(Players[player.Name]);
                ai.Attach(client);

                host.RegisterGameClient(client, player);
            }

            host.AcceptMessage(new BeginGameMessage());

            var firstClient = clients.First();

            while (!firstClient.GetGameState().Status.GameIsComplete)
                Thread.Sleep(500);

            var state = firstClient.GetGameState();

            File.WriteAllText(Path.Combine(Name, string.Format("game_{0}.txt", gameNumber)), state.Log);

            _results[gameNumber] = state.Results;            
        }

        private ResultsSummary CreateSummary()
        {
            List<GameResultsViewModel> resultsCopy;
            lock (_results)
                resultsCopy = _results.Where(r => r != null).ToList();

            var summary = new ResultsSummary();
            foreach (var kvp in Players)
            {
                string player = kvp.Key;
                var winPercentage = ((decimal)resultsCopy.Count(x => x.Winner == player) / resultsCopy.Count()) * 100.0m;
                var totalScore = resultsCopy.Sum(x => x.Scores.Single(p => p.PlayerName == player).Score);
                summary.AddResult(player, winPercentage, totalScore);
            }
            
            summary.CompletedGameCount = resultsCopy.Count();
            
            return summary;
        }

        private void WriteSummaryToFile(ResultsSummary summary)
        {
            var builder = new StringBuilder();
            foreach (var gameResult in summary.Results)
            {
                builder.AppendFormat("{0} Win %: {1} Total Score: {2}", gameResult.PlayerName, gameResult.WinPercentage,
                                     gameResult.TotalScore)
                    .AppendLine();
            }

            File.WriteAllText(Path.Combine(Name, "summary.txt"), builder.ToString());
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
            Results.Add(new PlayerResults { PlayerName = playerName, WinPercentage = winPercentage, TotalScore = totalScore });
        }

        public class PlayerResults
        {
            public string PlayerName { get; set; }
            public decimal WinPercentage { get; set; }
            public long TotalScore { get; set; }
        }
    }
}