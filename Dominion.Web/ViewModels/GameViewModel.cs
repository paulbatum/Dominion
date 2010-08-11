using System;
using System.Linq;

namespace Dominion.Web.ViewModels
{
    public class GameViewModel
    {
        public TurnContextViewModel TurnContext { get; set; }
        public CardPileViewModel[] Market { get; set; }
        public CardViewModel[] Hand { get; set; }
    }

    public class TurnContextViewModel
    {
        public int MoneyToSpend { get; set; }
        public int RemainingActions { get; set; }
        public int Buys { get; set; }
    }

    public class CardPileViewModel
    {
        public CardViewModel Card { get; set;}        
        public int Count { get; set; }
    }

    public class CardViewModel
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public string ImageUrl { get; set; }
    }
}