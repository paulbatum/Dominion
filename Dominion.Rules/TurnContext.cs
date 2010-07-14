using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules
{
    public class TurnContext
    {
        public TurnContext(Player player)
        {
            Player = player;
            PlayArea = new CardZone();
            MoneyToSpend = 0;
            RemainingActions = 1;
            Buys = 1;
            InBuyStep = false;
        }

        public Player Player { get; private set; }
        public CardZone PlayArea { get; private set; }
        public int MoneyToSpend { get; set; }
        public int RemainingActions { get; set; }
        public int Buys { get; set; }
        public bool InBuyStep { get; set; }
        
        public void DrawCards(int numberOfCardsToDraw)
        {
            Player.Deck.MoveCards(Player.Hand, numberOfCardsToDraw);
        }

        public bool CanPlay(ActionCard card)
        {
            return card.CanPlay(this);
        }

        public void Play(ActionCard card)
        {
            if(InBuyStep)
                throw new InvalidOperationException("Cannot play cards once in buy step");

            if(!card.CanPlay(this))
                throw new ArgumentException(string.Format("The card '{0}' cannot be played", card), "card");

            RemainingActions--;
            card.Play(this);

            card.MoveTo(PlayArea);
        }

        public void Buy(Card cardToBuy)
        {
            if (!InBuyStep)
                throw new InvalidOperationException("Cannot buy cards until you are in buy step");

            if(Buys < 1) 
                throw new ArgumentException(string.Format("Cannot buy the card '{0}' - no more buys.", cardToBuy));

            if (MoneyToSpend < cardToBuy.Cost)
                throw new ArgumentException(string.Format("Cannot buy the card '{0}', you only have {1} to spend.", cardToBuy, cardToBuy.Cost));

            Buys--;
            MoneyToSpend -= cardToBuy.Cost;
            RemainingActions = 0;
            
            cardToBuy.MoveTo(this.Player.Discards);
        }

        public void EndTurn()
        {
            PlayArea.MoveAll(Player.Discards);
            Player.Hand.MoveAll(Player.Discards);
            DrawCards(5);
        }

        public void MoveToBuyStep()
        {
            InBuyStep = true;
            MoneyToSpend = MoneyToSpend + this.Player.Hand.OfType<MoneyCard>().Select(x => x.Value).Sum();
        }
    }
}