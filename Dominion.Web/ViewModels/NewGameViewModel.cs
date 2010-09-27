using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominion.Web.ViewModels
{
    public class NewGameViewModel
    {
        public int NumberOfPlayers { get; set; }

        /// <summary>
        /// A comma delimited list of player names, or types of AI players.
        /// </summary>
        public string Names { get; set; }

        public IList<string> CardsToChooseFrom { get; set; }

        public IList<string> ChosenCards { get; set; }
    }
}