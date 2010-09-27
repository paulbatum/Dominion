using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Dominion.Cards.Actions;
using Dominion.Rules;

namespace Dominion.GameHost
{
    public class ChosenStartingConfiguration : StartingConfiguration
    {
        IList<Type> mChosenCardTypes;

        public ChosenStartingConfiguration(int numberOfPlayers, IEnumerable<string> chosenCards)
            : base(numberOfPlayers)
        {
            if (chosenCards.Count() != 10)
            {
                string error = string.Format("Passed card collection contains {0} cards. Expected exactly 10.", chosenCards.Count());
                throw new Exception(error);
            }

            var allCards = Assembly.GetAssembly(typeof(Festival))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Card)));

            mChosenCardTypes = chosenCards.Select(str => allCards.Single(c => c.Name == str)).ToList();
        }

        public override void InitializeBank(CardBank bank)
        {
            foreach (var cardType in mChosenCardTypes)
                bank.AddCardPile(new LimitedSupplyCardPile().WithNewCards(cardType, 10));

            base.InitializeBank(bank);
        }
    }
}
