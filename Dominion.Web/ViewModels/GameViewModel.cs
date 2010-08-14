using System;
using System.Linq;
using System.Web.Mvc;
using Dominion.GameHost;
using Dominion.Rules;

namespace Dominion.Web.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel(Game game, UrlHelper urlHelper)
        {
            Bank = game.Bank.Piles
                .Select(p => new CardPileViewModel(p, urlHelper)).ToArray();

            Hand = game.ActivePlayer.Hand
                .Select(c => new CardViewModel(c, urlHelper)).ToArray();

            InPlay = game.ActivePlayer.PlayArea
                .Select(c => new CardViewModel(c, urlHelper)).ToArray();

            Status = new TurnContextViewModel(game.CurrentTurn);

            Deck = new DeckViewModel(game.ActivePlayer.Deck, urlHelper);

            Discards = new DiscardPileViewModel(game.ActivePlayer.Discards, urlHelper);
        }

        public TurnContextViewModel Status { get; set; }
        public CardPileViewModel[] Bank { get; set; }
        public CardViewModel[] Hand { get; set; }
        public CardViewModel[] InPlay { get; set; }
        public DeckViewModel Deck { get; set; }
        public DiscardPileViewModel Discards { get; set; }
    }

    public class TurnContextViewModel
    {
        public TurnContextViewModel(TurnContext currentTurn)
        {
            BuyCount = currentTurn.Buys;
            RemainingActions = currentTurn.RemainingActions;
            MoneyToSpend = currentTurn.MoneyToSpend;
        }

        public int MoneyToSpend { get; set; }
        public int RemainingActions { get; set; }
        public int BuyCount { get; set; }
    }

    public class CardPileViewModel
    {
        private readonly UrlHelper _urlHelper;

        public CardPileViewModel(CardPile pile, UrlHelper urlHelper)
        {
            _urlHelper = urlHelper;

            Id = pile.Id.ToString();
            IsLimited = pile.IsLimited;
            Count = pile.IsLimited ? pile.CardCount : 0;

            if (!pile.IsEmpty)
            {
                Cost = pile.TopCard.Cost;
                Name = pile.TopCard.Name;
            }
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Count { get; set; }        
        public bool IsLimited { get; set; }

        public string ImageUrl
        {
            get { return _urlHelper.ResolveCardImage(Name); }
        }

        public string CountDescription
        {
            get { return IsLimited ? Count.ToString() : "Unlimited"; }
        }
    }

    public class CardViewModel
    {
        private readonly UrlHelper _urlHelper;

        public CardViewModel(Card card, UrlHelper urlHelper)
        {
            _urlHelper = urlHelper;

            Id = card.Id.ToString();
            Cost = card.Cost;
            Name = card.Name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }

        public string ImageUrl
        {
            get { return _urlHelper.ResolveCardImage(Name); }
        }
    }

    public class DeckViewModel
    {
        private readonly UrlHelper _urlHelper;

        public DeckViewModel(DrawDeck deck, UrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
            CountDescription = deck.CardCount.ToString();
            IsEmpty = deck.CardCount == 0;
        }

        public string CountDescription { get; set; }
        public bool IsEmpty { get; set; }

        public string ImageUrl
        {
            get
            {
                return _urlHelper.ResolveCardImage(IsEmpty ? "empty" : "deck");
            }
        }
        
    }

    public class DiscardPileViewModel
    {
        private readonly UrlHelper _urlHelper;

        public DiscardPileViewModel(DiscardPile discards, UrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
            IsEmpty = discards.CardCount == 0;
            CountDescription = discards.CardCount.ToString();

            if (!IsEmpty)
                TopCardName = discards.TopCard.Name;
        }

        public string CountDescription { get; set; }
        public bool IsEmpty { get; set; }
        public string TopCardName { get; set;}

        public string ImageUrl
        {
            get { return _urlHelper.ResolveCardImage(IsEmpty ? "empty" : TopCardName); }
        }
    }

    public static class CardImageHelper
    {
        public static string ResolveCardImage(this UrlHelper urlHelper, string cardName)
        {
            return urlHelper.Content(string.Format("~/Content/Images/Cards/{0}.jpg", cardName));
        }
    }
}