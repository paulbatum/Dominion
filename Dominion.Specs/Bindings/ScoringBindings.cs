using System;
using System.Linq;
using Dominion.Rules;
using TechTalk.SpecFlow;

namespace Dominion.Specs.Bindings
{
    [Binding]
    public class ScoringBindings : BindingBase
    {
        private GameScores _scores;
        private Game _game;

        [When(@"The game is scored")]
        public void WhenTheGameIsScored()
        {
            var gameBinding = Binding<GameStateBindings>();
            _game = gameBinding.Game;
            _game.Score();
            _scores = _game.Scores;
        }

        [Then(@"(.*) should have (\d+) victory point[s]?")]
        public void ThenPlayerShouldHaveVictoryPoints(string playerName, int victoryPoints)        
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            _scores[player].ShouldEqual(victoryPoints);
        }

        [Then(@"(.*)'s play area should start with this sequence of cards: (.*)")]              
        public void ThenPlayerPlayAreaShouldStartWithThisSequenceOfCards(string playerName, string sequence)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            string playAreaCardNames = string.Join(",", player.PlayArea.Select(c => c.Name).ToArray());

            playAreaCardNames.ShouldStartWith(sequence.Replace(" ",""));
        }

        
    }
}