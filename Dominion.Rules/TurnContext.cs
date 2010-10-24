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
            AvailableSpend = 0;
            RemainingActions = 1;
            Buys = 1;

            foreach (var longLivedEffect in player.LongLivedEffects)
                longLivedEffect.OnTurnStarting(this);
        }

        private Stack<ICardEffect> _effects = new Stack<ICardEffect>();
        private List<IPassiveCardEffect> _passiveEffects = new List<IPassiveCardEffect>();

        public Player ActivePlayer { get; private set; }
        public Game Game { get; private set; }
        public CardCost AvailableSpend { get; set; }
        public int RemainingActions { get; set; }
        public int Buys { get; set; }
        public bool InBuyStep { get; private set; }

        public IEnumerable<Player> Opponents
        {
            get
            {
                var playersBefore = this.Game.Players.TakeWhile(p => p != this.ActivePlayer);
                var playersAfter = this.Game.Players.SkipWhile(p => p != this.ActivePlayer).Skip(1);
                var opponents = playersAfter.Concat(playersBefore).ToList();
                return opponents;
            }
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

            return AvailableSpend.IsEnoughFor(pile.TopCard);
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
            AvailableSpend -= cardToBuy.Cost;
            this.Game.Log.LogBuy(this.ActivePlayer, pile);

            cardToBuy.MoveTo(this.ActivePlayer.Discards);
        }

        internal void EndTurn()
        {
            if (GetCurrentEffect() != null)
                throw new InvalidOperationException("Cannot end the turn - there is a current effect.");

            ActivePlayer.PlayArea.MoveWhere(c => !ShouldRemainInPlay(c), ActivePlayer.Discards);
            ActivePlayer.LongLivedEffects.RemoveAll(e => e.IsFinished);
            ActivePlayer.Hand.MoveAll(ActivePlayer.Discards);
            _passiveEffects.Clear();
            DrawCards(5);
        }

        private bool ShouldRemainInPlay(ICard card)
        {
            var relevantLongLivedEffects = ActivePlayer.LongLivedEffects.Where(c => c.SourceCard == card);
            if (relevantLongLivedEffects.Any(c => !c.IsFinished))
                return true;

            return false;
        }

        public void MoveToBuyStep()
        {
            if (GetCurrentEffect() != null)
                throw new InvalidOperationException("Cannot enter the buy step - there is a current effect.");

            if (InBuyStep)
                throw new InvalidOperationException("Cannot enter the buy step - already in buy step.");

            InBuyStep = true;
            RemainingActions = 0;

            foreach(var card in ActivePlayer.Hand.OfType<ITreasureCard>())
                AvailableSpend += CalculateAvailableSpend(card);
        }

        private CardCost CalculateAvailableSpend(ITreasureCard card)
        {
            CardCost value = card.Value;
            foreach (IPassiveCardEffect effect in _passiveEffects)
                value = effect.ModifyValue(value, card);

            return value;
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

                // Resolving one effect could push another on top.
                if (effect != _effects.Peek())
                    continue;

                if (effect.HasFinished)
                    _effects.Pop();
                else
                    return effect;
            }

            return null;
        }

        public void AddPassiveCardEffect(IPassiveCardEffect passiveCardEffect)
        {
            _passiveEffects.Add(passiveCardEffect);
        }

        public IEnumerable<IPassiveCardEffect> PassiveEffects
        {
            get
            {
                return _passiveEffects.AsReadOnly();
            }
        }

        public void AddLongLivedEffect(ILongLivedCardEffect effect)
        {
            ActivePlayer.LongLivedEffects.Add(effect);
        }

        public void Trash(Player player, ICard card)
        {
            this.Game.Log.LogTrash(player, card);
            card.MoveTo(this.Game.Trash);
        }

        public void DiscardCard(Player player, ICard card)
        {
            Game.Log.LogDiscard(player, card);
            card.MoveTo(player.Discards);
        }

        public void DiscardCards(Player player, IEnumerable<ICard> cards)
        {
            foreach (var card in cards)
                DiscardCard(player, card);
        }
    }

}