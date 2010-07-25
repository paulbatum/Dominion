using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dominion.Cards.Victory;
using Dominion.GameHost;
using Dominion.Rules;
using NUnit.Framework;
using Dominion.Cards.Treasure;

namespace Dominion.Tests
{
    [TestFixture]
    public class GameScorerTests
    {
        [Test]
        public void Zero_victory_cards_is_worth_zero_points()
        {
            var cards = new CardZone().AddNewCards<Copper>(10);
            var scorer = new GameScorer(cards);
            scorer.CalculateScore().ShouldEqual(0);
        }

        [Test]
        public void Three_estates_is_worth_three_points()
        {
            var cards = new CardZone().AddNewCards<Estate>(3);
            var scorer = new GameScorer(cards);
            scorer.CalculateScore().ShouldEqual(3);
        }

        [Test]
        public void Should_score_cards_regardless_of_current_zone()
        {
            var player = new Player("player", 10.NewCards<Copper>());
            player.Hand.AddNewCards<Estate>(1);
            player.Discards.AddNewCards<Estate>(1);
            player.Deck.AddNewCards<Estate>(1);

            player.CreateScorer().CalculateScore().ShouldEqual(3);
        }
    }
}