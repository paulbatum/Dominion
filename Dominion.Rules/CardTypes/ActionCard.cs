namespace Dominion.Rules.CardTypes
{
    public abstract class ActionCard : Card
    {
        protected ActionCard(int cost) : base(cost)
        {}

        protected internal virtual bool CanPlay(TurnContext context)
        {
            return context.RemainingActions > 0;
        }

        protected internal abstract void Play(TurnContext context);

        public void PlayFromOtherAction(TurnContext context)
        {
            Play(context);
        }
    }
}