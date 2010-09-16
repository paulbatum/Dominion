namespace Dominion.Rules.Activities
{
    public abstract class ActivityBase : IActivity
    {
        protected IGameLog Log { get; private set; }
        public Player Player { get; private set; }
        public string Message { get; private set; }
        public ActivityType Type { get; private set; }

        public bool IsSatisfied { get; protected set; }
        
        protected ActivityBase(IGameLog log, Player player, string message, ActivityType type)
        {
            Log = log;
            Player = player;
            Message = message;
            Type = type;
        }
    }

    public enum ActivityType
    {
        SelectFixedNumberOfCards,
        MakeYesNoChoice,
        WaitingForOtherPlayers
    }

    public enum RestrictionType
    {
        ActionCard
    }
}