using System;
using System.Linq;
using Dominion.GameHost;

namespace Dominion.Web.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel(IGameHost gameHost)
        {
            Bank = gameHost.CurrentGame.Bank.Piles.Select(p => new CardPileViewModel
            {
                Id = p.Id.ToString(),
                Cost = p.TopCard.Cost,
                IsLimited = p.IsLimited,
                Count = p.IsLimited ? p.CardCount : 0,
                Name = p.TopCard.Name
            }).ToArray();

            Hand = gameHost.CurrentGame.ActivePlayer.Hand.Select(c => new CardViewModel
            {
                Id = c.Id.ToString(),
                Cost = c.Cost,
                Name = c.Name
            }).ToArray();

            Status = new TurnContextViewModel()
            {
                BuyCount = gameHost.CurrentTurn.Buys,
                MoneyToSpend = gameHost.CurrentTurn.MoneyToSpend,
                RemainingActions = gameHost.CurrentTurn.RemainingActions
            };    
        }

        public TurnContextViewModel Status { get; set; }
        public CardPileViewModel[] Bank { get; set; }
        public CardViewModel[] Hand { get; set; }
    }

    public class TurnContextViewModel
    {
        public int MoneyToSpend { get; set; }
        public int RemainingActions { get; set; }
        public int BuyCount { get; set; }
    }

    public class CardPileViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }        
        public int Count { get; set; }
        public bool IsLimited { get; set; }

        public string ImageUrl
        {
            get { return Name.ResolveCardImage(); }
        }
    }

    public class CardViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }

        public string ImageUrl
        {
            get { return Name.ResolveCardImage(); }
        } 
    }

    public static class CardImageHelper
    {
        public static string ResolveCardImage(this string cardName)
        {
            return string.Format(@"/Content/Images/Cards/{0}.jpg", cardName);
        }
    }
}