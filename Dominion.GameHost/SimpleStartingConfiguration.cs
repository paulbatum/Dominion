using Dominion.Rules;
using Dominion.Cards.Actions;

namespace Dominion.GameHost
{
    public class SimpleStartingConfiguration : StartingConfiguration
    {
        public SimpleStartingConfiguration(int numberOfPlayers) : base(numberOfPlayers)
        {}

        public override void InitializeBank(Dominion.Rules.CardBank bank)
        {            
            bank.AddCardPile(new LimitedSupplyCardPile().WithNewCards<Smithy>(10));
            bank.AddCardPile(new LimitedSupplyCardPile().WithNewCards<Village>(10));
            bank.AddCardPile(new LimitedSupplyCardPile().WithNewCards<Woodcutter>(10));
            bank.AddCardPile(new LimitedSupplyCardPile().WithNewCards<Market>(10));
            bank.AddCardPile(new LimitedSupplyCardPile().WithNewCards<Witch>(10));
            bank.AddCardPile(new LimitedSupplyCardPile().WithNewCards<CouncilRoom>(10));
            bank.AddCardPile(new LimitedSupplyCardPile().WithNewCards<Militia>(10));
            bank.AddCardPile(new LimitedSupplyCardPile().WithNewCards<Chancellor>(10));
            bank.AddCardPile(new LimitedSupplyCardPile().WithNewCards<Woodcutter>(10));
            bank.AddCardPile(new LimitedSupplyCardPile().WithNewCards<Smithy>(10));
            base.InitializeBank(bank);
        }

    }
}