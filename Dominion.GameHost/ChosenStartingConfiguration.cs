using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Dominion.Cards.Actions;
using Dominion.Cards.Treasure;
using Dominion.Rules;

namespace Dominion.GameHost
{
    public class ChosenStartingConfiguration : StartingConfiguration
    {
        private readonly bool _useProsperity;
        IList<string> _chosenCards;

        public ChosenStartingConfiguration(int numberOfPlayers, IEnumerable<string> chosenCards, bool useProsperity)
            : base(numberOfPlayers)
        {
            _useProsperity = useProsperity;
            if (chosenCards.Count() != 10)
            {
                string error = string.Format("Passed card collection contains {0} cards. Expected exactly 10.", chosenCards.Count());
                throw new ArgumentException(error, "chosenCards");
            }           

            _chosenCards = chosenCards.ToList();
        }

        public override void InitializeBank(CardBank bank)
        {
            foreach (string card in _chosenCards)
                bank.AddCardPile(new LimitedSupplyCardPile().WithNewCards(card, GetStartingCount(card)));

            if (_useProsperity)            
                AddProsperityCards(bank);                            

            base.InitializeBank(bank);
        }

        
    }
}
