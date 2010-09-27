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

        public bool CanPlay(ICard card)
        {
            if (GetCurrentEffect() != null)
                return false;

            if (InBuyStep)
                return false;

            var actionCard = card as IActionCard;
            if (actionCard != null)
                return this.RemainingActions > 0;

            return false;
        }

        public bool CanPlay(ICard card, Player player)
        {
            return this.ActivePlayer == player && CanPlay(card);
        }

        public void Play(IActionCard card)
        {
            if (!CanPlay(card))
                throw new ArgumentException(string.Format("The card '{0}' cannot be played", card), "card");

            RemainingActions--;
            this.Game.Log.LogPlay(this.ActivePlayer, card);
            card.MoveTo(ActivePlayer.PlayArea);
            card.Play(this);
            ResolvePendingEffects();
        }

        // Side effects FTW.
        private void ResolvePendingEffects()
        {
            GetCurrentEffect();
        }

        public bool CanBuy(CardPile pile)
        {
            if (GetCurrentEffect() != null)
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

        public bool CanBuy(CardPile pile, Player player)
        {
            return this.ActivePlayer == player && CanBuy(pile);
        }

        public void Buy(CardPile pile)
        {
            if (!CanBuy(pile))
                throw new InvalidOperationException("Cannot buy card.");

            var cardToBuy = pile.TopCard;

            Buys--;
            MoneyToSpend -= cardToBuy.Cost;
            this.Game.Log.LogBuy(this.ActivePlayer, pile);

            cardToBuy.MoveTo(this.ActivePlayer.Discards);
        }

        internal void EndTurn()
        {
            if (GetCurrentEffect() != null)
                throw new InvalidOperationException("Cannot end the turn - there is a current effect.");

            ActivePlayer.PlayArea.MoveAll(ActivePlayer.Discards);
            ActivePlayer.Hand.MoveAll(ActivePlayer.Discards);
            DrawCards(5);
        }

        public void MoveToBuyStep()
        {
            if (GetCurrentEffect() != null)
                throw new InvalidOperationException("Cannot enter the buy step - there is a current effect.");

            if (InBuyStep)
                throw new InvalidOperationException("Cannot enter the buy step - already in buy step.");

            InBuyStep = true;
            RemainingActions = 0;
            MoneyToSpend = MoneyToSpend + this.ActivePlayer.Hand.OfType<IMoneyCard>().Sum(x => x.Value);
        }

        public void AddEffect(ICardEffect cardEffect)
        {
            _effects.Push(cardEffect);
        }

        public ICardEffect GetCurrentEffect()
        {

            while (_effects.Count > 0)
            {
                var effect = _effects.Peek();

                effect.BeginResolve(this);

                if (effect.HasFinished)
                    _effects.Pop();
                else
                    return effect;
            }

            return null;
        }

        public void Trash(Player player, ICard card)
        {
            this.Game.Log.LogTrash(player, card);
            card.MoveTo(this.Game.Trash);
        }


        public void DiscardCards(Player player, IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                Game.Log.LogDiscard(player, card);
                card.MoveTo(player.Discards);
            }
        }
    }

}