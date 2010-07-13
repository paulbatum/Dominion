namespace Dominion.Rules.CardTypes
{
    public abstract class ActionCard : Card
    {
        public override bool CanPlay(TurnContext context)
        {
            return context.RemainingActions > 0;
        }
    }
}