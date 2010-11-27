using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.GameHost
{
    public class GameViewModel
    {
        public GameViewModel(Game game, Player player)
        {
            Log = game.Log.Contents;

            Version = game.Version;
            
            PopulateActivityRelated(game, player);

            Bank = game.Bank.Piles
                .Select(p => new CardPileViewModel(p, game.CurrentTurn, player)).ToArray();

            Hand = player.Hand
                .Select(c => new CardViewModel(c, game.CurrentTurn, player)).ToArray();

            if (game.IsComplete)
            {
                InPlay = player.PlayArea
                    .Select(c => new CardViewModel(c)).ToArray();
            }
            else
            {
                InPlay = game.ActivePlayer.PlayArea
                    .Select(c => new CardViewModel(c)).ToArray();
            }

            Status = new TurnContextViewModel(game.CurrentTurn, player);

            Deck = new DeckViewModel(player.Deck);

            Discards = new DiscardPileViewModel(player.Discards);
        }

        private void PopulateActivityRelated(Game game, Player player)
        {
            var activity = game.GetPendingActivity(player);

            if(activity != null)
                PendingActivity = new ActivityModel(activity);

            if (activity is IRevealedCardsActivity)
            {
                this.Revealed = ((IRevealedCardsActivity)activity).RevealedCards
                    .Select(c => new CardViewModel(c)).ToArray();
            }
        }

        public string Log { get; set; }
        public long Version { get; set; }
        public ActivityModel PendingActivity { get; set; }
        public TurnContextViewModel Status { get; set; }
        public CardPileViewModel[] Bank { get; set; }
        public CardViewModel[] Hand { get; set; }
        public CardViewModel[] InPlay { get; set; }
        public DeckViewModel Deck { get; set; }
        public DiscardPileViewModel Discards { get; set; }
        public CardViewModel[] Revealed { get; set; }
    }

    public class ActivityModel
    {
        public ActivityModel(IActivity activity)
        {
            Properties = activity.Properties;
            Type = activity.Type.ToString();            
            Message = activity.Message;
            Id = activity.Id;

            Hint = activity.Hint.ToString();
            Source = activity.Source;
        }
        
        public string Type { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string Hint { get; set; }
        public Guid Id { get; set; }
        public IDictionary<string, object> Properties { get; set; }
    }

    public class TurnContextViewModel
    {
        public TurnContextViewModel(TurnContext currentTurn, Player player)
        {
            GameIsComplete = currentTurn.Game.IsComplete;
            
            if(currentTurn.ActivePlayer == player && !GameIsComplete)
            {
                BuyCount = currentTurn.Buys;
                RemainingActions = currentTurn.RemainingActions;
                AvailableSpend = new AvailableSpendViewModel(currentTurn.AvailableSpend);
                IsActive = true;
                InBuyStep = currentTurn.InBuyStep;
            }
            else
            {
                AvailableSpend = new AvailableSpendViewModel(new CardCost(0));
                IsActive = false;
            }

            ActivePlayerName = currentTurn.ActivePlayer.Name;
            
        }

        public bool GameIsComplete { get; set; }
        public bool IsActive { get; set; }
        public bool InBuyStep { get; set; }
        public AvailableSpendViewModel AvailableSpend { get; set; }
        public int RemainingActions { get; set; }
        public int BuyCount { get; set; }
        public string ActivePlayerName { get; set; }
    }

    public class AvailableSpendViewModel
    {
        public AvailableSpendViewModel(CardCost availableSpend)
        {
            Money = availableSpend.Money;
            Potions = availableSpend.Potions;
            DisplayValue = availableSpend.ToString();
        }

        public int Money { get; set; }
        public int Potions { get; set; }
        public string DisplayValue { get; set; }
    }

    public class CardPileViewModel
    {
        public CardPileViewModel(CardPile pile, TurnContext context, Player player)
        {
            Id = pile.Id;
            IsLimited = pile.IsLimited;
            Count = pile.IsLimited ? pile.CardCount : 0;
            Name = pile.Name;

            if (!pile.IsEmpty)
                Cost = pile.TopCard.Cost.Money;

            CanBuy = context.CanBuy(pile, player);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public CardCost Cost { get; set; }
        public int Count { get; set; }
        public bool IsLimited { get; set; }
        public bool CanBuy { get; set; }

        public string CountDescription
        {
            get { return IsLimited ? Count.ToString() : "∞"; }
        }
    }

    public class CardViewModel
    {
        public CardViewModel(ICard card)
        {
            Id = card.Id;
            Cost = card.Cost.Money;
            Name = card.Name;

            var types = new List<string>();
            if(card is IActionCard)
                types.Add("Action");
            if (card is IReactionCard)
                types.Add("Reaction");
            if (card is IAttackCard)
                types.Add("Attack");
            if (card is IVictoryCard)
                types.Add("Victory");
            if (card is ICurseCard)
                types.Add("Curse");
            if (card is ITreasureCard)
                types.Add("Treasure");

            Types = types.ToArray();
        }

        public CardViewModel(ICard card, TurnContext currentTurn, Player player) : this(card)
        {
            CanPlay = currentTurn.CanPlay(card, player);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public string[] Types { get; set; }
        public bool CanPlay { get; set; }

    }

    public class DeckViewModel
    {
        public DeckViewModel(DrawDeck deck)
        {
            CountDescription = deck.CardCount.ToString();
            IsEmpty = deck.CardCount == 0;
        }

        public string CountDescription { get; set; }
        public bool IsEmpty { get; set; }

    }

    public class DiscardPileViewModel
    {
        public DiscardPileViewModel(DiscardPile discards)
        {
            IsEmpty = discards.CardCount == 0;
            CountDescription = discards.CardCount.ToString();

            if (!IsEmpty)
                TopCardName = discards.TopCard.Name;
        }

        public string CountDescription { get; set; }
        public bool IsEmpty { get; set; }
        public string TopCardName { get; set; }
    }
}
