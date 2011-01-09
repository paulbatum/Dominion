namespace Dominion.Rules.Activities
{
    public interface ISelectFromRevealedCardsActivity : ISelectCardsActivity, IRevealedCardsActivity
    {
    }

    public interface IRevealedCardsActivity
    {
        RevealZone RevealedCards { get; }        
    }

    public class SelectFromRevealedCardsActivity : SelectCardsActivity, ISelectFromRevealedCardsActivity
    {
        public RevealZone RevealedCards { get; private set; }

        public SelectFromRevealedCardsActivity(IGameLog log, Player player, RevealZone revealZone, string message, ISelectionSpecification selectionSpecification, ICard source)
            : base(log, player, message, selectionSpecification, source)
        {
            RevealedCards = revealZone;

            // HACK. I'm ignoring the activity type on the selection specification. Not sure what to do here.
            Type = ActivityType.SelectFromRevealed;
        }
    }

    public class ChooseBasedOnRevealedCardsActivity : ChoiceActivity, IRevealedCardsActivity
    {
        public RevealZone RevealedCards { get; private set; }

        public ChooseBasedOnRevealedCardsActivity(TurnContext context, Player player, RevealZone revealZone, string message, ICard source, params Choice[] options) 
            : base(context, player, message, source, options)
        {
            RevealedCards = revealZone;                        
        }

        public ChooseBasedOnRevealedCardsActivity(IGameLog log, Player player, RevealZone revealZone, string message, ICard source, params Choice[] options) 
            : base(log, player, message, source, options)
        {
            RevealedCards = revealZone;
        }
    }
}