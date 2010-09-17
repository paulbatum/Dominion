using System;
using System.Collections.Generic;

namespace Dominion.Rules
{
    public class Player
    {
        public Player(string name, IEnumerable<Card> startingDeck)
        {
            Id = Guid.NewGuid();
            Name = name;
            Discards = new DiscardPile();

            Deck = new DrawDeck(startingDeck, Discards);
            Deck.Shuffle();

            Hand = new EnumerableCardZone();
            Deck.MoveCards(Hand, 5);

            PlayArea = new EnumerableCardZone();                       
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DrawDeck Deck { get; private set; }
        public DiscardPile Discards { get; private set; }
        public EnumerableCardZone Hand { get; private set; }
        public EnumerableCardZone PlayArea { get; private set; }

        public TurnContext BeginTurn(Game game)
        {            
            var context = new TurnContext(this, game);
            return context;
        }

        public PlayerScorer CreateScorer()
        {
            var scorer = new PlayerScorer(this);
            return scorer;
        }

        public void DrawCards(int numberOfCards)
        {
            var actualDrawCount = Math.Min(Deck.CardCount + Discards.CardCount,
                               numberOfCards);
            Deck.MoveCards(Hand, actualDrawCount);
        }
    }
}