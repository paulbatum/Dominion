using System.Collections.Generic;
using Dominion.Cards.Treasure;
using Dominion.Cards.Victory;
using Dominion.Rules;
using NUnit.Framework;


namespace Dominion.Tests
{
    [TestFixture]
    public class DeckTests
    {
        private Card _copper;
        private Card _estate;
        private DiscardPile _discardPile;

        [SetUp]
        public void SetUp()
        {
            _copper = new Copper();
            _estate = new Estate();
            _discardPile = new DiscardPile();
        }

        [Test]
        public void Should_add_cards_passed_via_constructor()
        {
            var drawDeck = new DrawDeck(new List<Card> { _copper, _estate }, _discardPile);
            drawDeck.CardCount.ShouldEqual(2);
            _copper.CurrentZone.ShouldEqual(drawDeck);
            _estate.CurrentZone.ShouldEqual(drawDeck);
        }

        [Test]
        public void Should_replace_top_card_when_moved()
        {
            var drawDeck = new DrawDeck(new List<Card> { _copper, _estate }, _discardPile);
            drawDeck.TopCard.ShouldEqual(_copper);
            drawDeck.TopCard.MoveTo(_discardPile);
            drawDeck.TopCard.ShouldEqual(_estate);
        }

        [Test]
        public void Should_NOT_refill_draw_deck_from_discards_when_empty()
        {
            var drawDeck = new DrawDeck(new List<Card> { _copper, _estate }, _discardPile);
            drawDeck.TopCard.MoveTo(_discardPile);
            drawDeck.TopCard.MoveTo(_discardPile);
            drawDeck.CardCount.ShouldEqual(0);
        }

        [Test]
        public void Should_refill_draw_deck_from_discards_when_card_required()
        {
            var drawDeck = new DrawDeck(new List<Card> { _copper, _estate }, _discardPile);
            var hand = new Hand();

            // First move the copper to the discard pile
            drawDeck.TopCard.MoveTo(_discardPile);

            // Next move the estate to the hand
            drawDeck.TopCard.MoveTo(hand);
            
            drawDeck.TopCard.ShouldEqual(_copper);
        }

        [Test]
        public void Should_shuffle_deck_after_being_refilled()
        {
            var cards = new List<Card>();
            15.Times(() => cards.Add(new Copper()));
            15.Times(() => cards.Add(new Estate()));

            var drawDeck = new DrawDeck(cards, _discardPile);

            // Move all the cards to the discard pile
            drawDeck.MoveAll(_discardPile);

            // Peek at the top card to trigger a shuffle
            var topCard = drawDeck.TopCard;

            var cardsAfterShuffle = new List<Card>();
            30.Times(() =>
            {
                cardsAfterShuffle.Add(drawDeck.TopCard);
                drawDeck.TopCard.MoveTo(_discardPile);
            });

            Assert.AreNotEqual(cards, cardsAfterShuffle);
        }
    }
}