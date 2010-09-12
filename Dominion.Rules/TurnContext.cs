using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules
{
    public class TurnContext
    {
        public TurnContext(Player player, Game game)
        {
            ActivePlayer = player;
            Game = game;
            MoneyToSpend = 0;
            RemainingActions = 1;
            Buys = 1;
        }

        public Player ActivePlayer { get; private set; }
        public Game Game { get; private set; }
        public int MoneyToSpend { get; set; }
        public int RemainingActions { get; set; }
        public int Buys { get; set; }
        public bool InBuyStep { get; private set; }
        public bool HasEnded { get; private set; }
        
        public void DrawCards(int numberOfCardsToDraw)
        {
            var actualDrawCount = Math.Min(ActivePlayer.Deck.CardCount + ActivePlayer.Discards.CardCount,
                                           numberOfCardsToDraw);
            ActivePlayer.Deck.MoveCards(ActivePlayer.Hand, actualDrawCount);
        }

        public bool CanPlay(Card card)
        {
            if (InBuyStep)
                return false;

            var actionCard = card as ActionCard;
            if (actionCard != null)                
                return actionCard.CanPlay(this);

            return false;
        }

        public void Play(ActionCard card)
        {
            if(!card.CanPlay(this))
                throw new ArgumentException(string.Format("The card '{0}' cannot be played", card), "card");

            RemainingActions--;
            this.Game.Log.LogPlay(this.ActivePlayer, card);
            card.Play(this);

            card.MoveTo(ActivePlayer.PlayArea);
        }

        public bool CanBuy(CardPile pile)
        {
            if (!InBuyStep)
                return false;
                //throw new InvalidOperationException("Cannot buy cards until you are in buy step");

            if (Buys < 1)
                return false;
                //throw new ArgumentException(string.Format("Cannot buy the card '{0}' - no more buys.", cardToBuy));

            if (pile.IsEmpty)
                return false;

            if (MoneyToSpend < pile.TopCard.Cost)
                return false;
                //throw new ArgumentException(string.Format("Cannot buy the card '{0}', you only have {1} to spend.", cardToBuy, MoneyToSpend));

            return true;
        }

        public void Buy(CardPile pile)
        {
            if(!CanBuy(pile))
                throw new InvalidOperationException("Cannot buy card.");

            var cardToBuy = pile.TopCard;

            Buys--;
            MoneyToSpend -= cardToBuy.Cost;
            this.Game.Log.LogBuy(this.ActivePlayer, pile);
            
            cardToBuy.MoveTo(this.ActivePlayer.Discards);
        }

        public void EndTurn()
        {
            ActivePlayer.PlayArea.MoveAll(ActivePlayer.Discards);
            ActivePlayer.Hand.MoveAll(ActivePlayer.Discards);
            DrawCards(5);
            HasEnded = true;
        }

        public void MoveToBuyStep()
        {
            if(InBuyStep)
                throw new InvalidOperationException("Cannot enter the buy step - already in buy step.");

            InBuyStep = true;
            RemainingActions = 0;
            MoneyToSpend = MoneyToSpend + this.ActivePlayer.Hand.OfType<MoneyCard>().Sum(x => x.Value);
        }
    }
}