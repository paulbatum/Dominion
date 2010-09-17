using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.Activities;
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

        private Stack<ICardEffect> _effects = new Stack<ICardEffect>();

        public Player ActivePlayer { get; private set; }
        public Game Game { get; private set; }
        public int MoneyToSpend { get; set; }
        public int RemainingActions { get; set; }
        public int Buys { get; set; }
        public bool InBuyStep { get; private set; }

        public IEnumerable<Player> Opponents
        {
            get { return this.Game.Players.Where(p => p != ActivePlayer); }
        }

        public void DrawCards(int numberOfCardsToDraw)
        {
            ActivePlayer.DrawCards(numberOfCardsToDraw);
        }

        public bool CanPlay(Card card)
        {
            if (CurrentEffect != null)
                return false;

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
            card.MoveTo(ActivePlayer.PlayArea);
            card.Play(this);
        }

        public bool CanBuy(CardPile pile)
        {
            if (CurrentEffect != null)
                return false;

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

        internal void EndTurn()
        {
            if (CurrentEffect != null)
                throw new InvalidOperationException("Cannot end the turn - there is a current effect.");

            ActivePlayer.PlayArea.MoveAll(ActivePlayer.Discards);
            ActivePlayer.Hand.MoveAll(ActivePlayer.Discards);
            DrawCards(5);            
        }

        public void MoveToBuyStep()
        {
            if (CurrentEffect != null)
                throw new InvalidOperationException("Cannot enter the buy step - there is a current effect.");

            if(InBuyStep)
                throw new InvalidOperationException("Cannot enter the buy step - already in buy step.");

            InBuyStep = true;
            RemainingActions = 0;
            MoneyToSpend = MoneyToSpend + this.ActivePlayer.Hand.OfType<MoneyCard>().Sum(x => x.Value);
        }

        public void AddEffect(ICardEffect cardEffect)
        {
            _effects.Push(cardEffect);
        }

        public ICardEffect CurrentEffect
        {
            get
            {
                while (_effects.Count > 0 && _effects.Peek().HasFinished)
                    _effects.Pop();

                if (_effects.Count == 0)
                    return null;

                return _effects.Peek();
            }
        }

        public void Trash(Player player, Card card)
        {
            this.Game.Log.LogTrash(player, card);
            card.MoveTo(this.Game.Trash);
        }
    }

}