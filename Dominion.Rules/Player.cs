namespace Dominion.Rules
{
    public class Player
    {
        public DrawDeck Deck { get; set; }
        public DiscardPile Discards { get; set; }
        public Hand Hand { get; set; }

        public TurnContext BeginTurn()
        {            
            var context = new TurnContext(this);
            return context;
        }
    }
}