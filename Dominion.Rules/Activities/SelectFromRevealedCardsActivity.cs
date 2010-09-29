namespace Dominion.Rules.Activities
{
    public interface ISelectFromRevealedCardsActivity : ISelectCardsActivity
    {
        RevealZone RevealedCards { get; }
    }

    public class SelectFromRevealedCardsActivity : SelectCardsActivity, ISelectFromRevealedCardsActivity
    {
        public RevealZone RevealedCards { get; private set; }

        public SelectFromRevealedCardsActivity(TurnContext context, RevealZone revealZone, string message, ISelectionSpecification selectionSpecification)
            : base(context.Game.Log, context.ActivePlayer, message, selectionSpecification)
        {
            RevealedCards = revealZone;
        }
    }
}