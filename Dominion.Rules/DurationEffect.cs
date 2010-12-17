namespace Dominion.Rules
{
    public abstract class DurationEffect : ILongLivedCardEffect
    {
        public DurationEffect(ICard sourceCard)
        {
            SourceCard = sourceCard;
            IsFinished = false;
        }

        public ICard SourceCard { get; set; }
        public bool IsFinished { get; set; }

        public virtual void OnTurnStarting(TurnContext context)
        {
            IsFinished = true;
        }


    }
}