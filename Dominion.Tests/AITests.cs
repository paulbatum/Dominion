using System.Diagnostics;
using System.Linq;
using System.Threading;
using Dominion.GameHost;
using Dominion.Rules;
using NUnit.Framework;

namespace Dominion.Tests
{
    [TestFixture, Explicit]
    public class AITests
    {
        [Test]
        public void BigMoneyAI_can_complete_a_game()
        {

            var multiGameHost = new MultiGameHost();
            var key = multiGameHost.CreateNewGame(new[] { "BigMoneyAI", "BigMoneyAI", "BigMoneyAI" }, 3);
            var gameData = multiGameHost.GetGameData(key);
            var player1Id = gameData.Slots.Keys.First();

            var player1Client = multiGameHost.FindClient(player1Id);
            while(!player1Client.GetGameState().Status.GameIsComplete)
                Thread.Sleep(500);

            Debug.Write(player1Client.GetGameState().Log);
        }

        [Test]
        public void MilitiaAI_can_complete_a_game()
        {

            var multiGameHost = new MultiGameHost();
            var key = multiGameHost.CreateNewGame(new[] { "MilitiaAI", "BigMoneyAI", "BigMoneyAI" }, 3);
            var gameData = multiGameHost.GetGameData(key);
            var player1Id = gameData.Slots.Keys.First();

            var player1Client = multiGameHost.FindClient(player1Id);
            while (!player1Client.GetGameState().Status.GameIsComplete)
                Thread.Sleep(500);

            Debug.Write(player1Client.GetGameState().Log);
        }

        [Test]
        public void SimpleAI_can_complete_a_game()
        {

            var multiGameHost = new MultiGameHost();
            var key = multiGameHost.CreateNewGame(new[] { "SimpleAI", "SimpleAI", "SimpleAI" }, 3);
            var gameData = multiGameHost.GetGameData(key);
            var player1Id = gameData.Slots.Keys.First();

            var player1Client = multiGameHost.FindClient(player1Id);
            while (!player1Client.GetGameState().Status.GameIsComplete)
                Thread.Sleep(500);

            Debug.Write(player1Client.GetGameState().Log);
        }

        [Test]
        public void BigMoney_vs_SimpleAI()
        {
            var multiGameHost = new MultiGameHost();
            var key = multiGameHost.CreateNewGame(new[] { "SimpleAI", "SimpleAI", "BigMoneyAI", "BigMoneyAI" }, 4);
            var gameData = multiGameHost.GetGameData(key);
            var player1Id = gameData.Slots.Keys.First();

            var player1Client = multiGameHost.FindClient(player1Id);
            while (!player1Client.GetGameState().Status.GameIsComplete)
                Thread.Sleep(500);

            Debug.Write(player1Client.GetGameState().Log);
        }
    }
}