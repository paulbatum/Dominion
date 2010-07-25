using System;
using System.Collections.Generic;

namespace Dominion.Rules
{
    public class Player
    {
        public Player(string name, IEnumerable<Card> startingDeck)
        {
            Name = name;
            Discards = new DiscardPile();

            Deck = new DrawDeck(startingDeck, Discards);
            Deck.Shuffle();

            Hand = new Hand();
            Deck.MoveCards(Hand, 5);

            PlayArea = new CardZone();
        }

        public string Name { get; private set; }
        public DrawDeck Deck { get; private set; }
        public DiscardPile Discards { get; private set; }
        public Hand Hand { get; private set; }
        public CardZone PlayArea { get; private set; }

        public TurnContext BeginTurn()
        {            
            var context = new TurnContext(this);
            return context;
        }

        public GameScorer CreateScorer()
        {
            var scorer = new GameScorer(this);
            return scorer;
        }

       
    }
}