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

            if (Status.GameIsComplete)
            {
                Results = new GameResultsViewModel(game.Scores);
            }
        }

        private void PopulateActivityRelated(Game game, Player player)
        {
            var activity = game.GetPendingActivity(player);

            if (activity != null)
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
        public GameResultsViewModel Results { get; set; }
    }

    public class ActivityModel
    {
        public ActivityModel(IActivity activity)
        {
            Properties = ConvertProperties(activity);
            Type = activity.Type.ToString();
            Message = activity.Message;
            Id = activity.Id;

            Hint = activity.Hint.ToString();
            Source = activity.Source;
        }

        private static IDictionary<string, object> ConvertProperties(IActivity activity)
        {
            // TODO: Here be dragons, etc. Really need a way to handle this better.
            var viewModelProperties = new Dictionary<string, object>(activity.Properties);
            if (activity.Properties.ContainsKey("CardsMustBeOfType"))
            {
                viewModelProperties["CardsMustBeOfType"] =
                    ((Type)viewModelProperties["CardsMustBeOfType"]).GetCardTypes().Single();
            }
            return viewModelProperties;
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

            if (currentTurn.ActivePlayer == player && !GameIsComplete)
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

            if (pile.IsEmpty)
            {
                Cost = 0;
                Types = new string[] { };
            }
            else
            {
                Cost = pile.TopCard.Cost;
                Types = pile.TopCard.GetCardTypes();
            }

            var activity = context.Game.GetPendingActivity(player) as SelectPileActivity;
            var canGain = activity != null && activity.Specification.IsMatch(pile);
            CanBuy = canGain || context.CanBuy(pile, player);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public CardCost Cost { get; set; }
        public int Count { get; set; }
        public bool IsLimited { get; set; }
        public bool CanBuy { get; set; }
        public string[] Types { get; set; }

        public string CountDescription
        {
            get { return IsLimited ? Count.ToString() : "∞"; }
        }
    }

    public enum CardType
    {
        Action,
        Reaction,
        Attack,
        Victory,
        Curse,
        Treasure
    }

    public class CardViewModel
    {
        public CardViewModel(ICard card)
        {
            Id = card.Id;
            Cost = card.Cost.Money;
            Name = card.Name;
            Types = card.GetCardTypes();
        }

        public CardViewModel(ICard card, TurnContext currentTurn, Player player)
            : this(card)
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

    public class GameResultsViewModel
    {
        public GameResultsViewModel(GameScores scores)
        {
            Winner = scores.Winner.Name;
            Scores = scores
                .Select(s => new PlayerResultViewModel { PlayerName = s.Key.Name, Score = s.Value })
                .ToArray();
        }

        public string Winner { get; set; }
        public PlayerResultViewModel[] Scores { get; set; }

        public class PlayerResultViewModel
        {
            public string PlayerName { get; set; }
            public int Score { get; set; }
        }
    }



    public static class ViewModelExtensions
    {
        public static string[] GetCardTypes(this ICard card)
        {
            return card.GetType().GetCardTypes();
        }

        private static bool ImplementsOrIsInterface<TInterface>(this Type type)
        {
            return (type.IsInterface && type == typeof(TInterface))
               || type.GetInterfaces().Contains(typeof(TInterface));
        }

        public static string[] GetCardTypes(this Type type)
        {
            var types = new List<CardType>();
            if (type.ImplementsOrIsInterface<IActionCard>())
                types.Add(CardType.Action);
            if (type.ImplementsOrIsInterface<IReactionCard>())
                types.Add(CardType.Reaction);
            if (type.ImplementsOrIsInterface<IAttackCard>())
                types.Add(CardType.Attack);
            if (type.ImplementsOrIsInterface<IVictoryCard>())
                types.Add(CardType.Victory);
            if (type.ImplementsOrIsInterface<ICurseCard>())
                types.Add(CardType.Curse);
            if (type.ImplementsOrIsInterface<ITreasureCard>())
                types.Add(CardType.Treasure);

            return types.Select(t => t.ToString()).ToArray();
        }
    }
}
