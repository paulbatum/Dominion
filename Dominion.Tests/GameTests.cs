using System.Collections.Generic;
using System.Linq;
using Dominion.GameHost;
using Dominion.Rules;
using NUnit.Framework;
namespace Dominion.Tests
{
    [TestFixture]
    public class GameTests
    {
        private Player _paul;
        private Player _tristan;
        private Player _anatoli;
        private IList<Player> _players;
        private Game _game;

        [SetUp]
        public void SetUp()
        {
            var config = new SimpleStartingConfiguration(3);

            _paul = new Player("Paul", config.CreateStartingDeck());
            _tristan = new Player("Tristan", config.CreateStartingDeck());
            _anatoli = new Player("Anatoli", config.CreateStartingDeck());

            _players = new List<Player>(new[] { _paul, _tristan, _anatoli });
            _game = new Game(_players, new CardBank());
        }

        [Test]
        public void Should_give_first_player_the_first_turn()
        {
            var firstTurn = _game.GameTurns().First();
            var firstPlayer = _players.First();

            firstTurn.ActivePlayer.ShouldEqual(firstPlayer);
            _game.ActivePlayer.ShouldEqual(firstPlayer);
        }

        [Test]
        public void Should_give_second_player_the_second_turn()
        {
            var secondTurn = _game.GameTurns().Skip(1).First();
            var secondPlayer = _players.Skip(1).First();

            secondTurn.ActivePlayer.ShouldEqual(secondPlayer);
            _game.ActivePlayer.ShouldEqual(secondPlayer);
        }

        [Test]
        public void Should_give_third_player_the_third_turn()
        {
            var thirdTurn = _game.GameTurns().Skip(2).First();
            var thirdPlayer = _players.Skip(2).First();

            thirdTurn.ActivePlayer.ShouldEqual(thirdPlayer);
            _game.ActivePlayer.ShouldEqual(thirdPlayer);
        }

        [Test]
        public void Should_give_the_first_player_another_turn()
        {
            var turn = _game.GameTurns().Skip(_players.Count).First();
            var firstPlayer = _players.First();

            turn.ActivePlayer.ShouldEqual(firstPlayer);
            _game.ActivePlayer.ShouldEqual(firstPlayer);
        }

        [Test]
        public void Game_with_three_players_should_be_over_when_there_are_three_empty_piles()
        {
            _game.Bank.AddCardPile(new LimitedSupplyCardPile());
            _game.Bank.AddCardPile(new LimitedSupplyCardPile());
            _game.Bank.AddCardPile(new LimitedSupplyCardPile());

            _game.IsComplete.ShouldBeTrue();
        }

        [Test]
        public void Game_should_be_over_when_a_key_pile_is_empty()
        {
            _game.Bank.AddCardPileWhichEndsTheGameWhenEmpty(new LimitedSupplyCardPile());
            _game.IsComplete.ShouldBeTrue();
        }

        [Test]
        public void Game_with_three_players_should_not_be_over_when_there_are_two_empty_piles()
        {
            _game.Bank.AddCardPile(new LimitedSupplyCardPile());
            _game.Bank.AddCardPile(new LimitedSupplyCardPile());
            _game.IsComplete.ShouldBeFalse();
        }
    }
}