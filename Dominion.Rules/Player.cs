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

            Hand = new Hand();
            Deck.MoveCards(Hand, 5);

            PlayArea = new PlayArea();                       
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DrawDeck Deck { get; private set; }
        public DiscardPile Discards { get; private set; }
        public Hand Hand { get; private set; }
        public PlayArea PlayArea { get; private set; }

        public TurnContext BeginTurn(Game game)
        {            
            var context = new TurnContext(this, game);
            return context;
        }

        public GameScorer CreateScorer()
        {
            var scorer = new GameScorer(this);
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