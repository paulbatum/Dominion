namespace Dominion.Rules.Activities
{
    public class PlayActionsActivity : ActivityBase
    {
        public PlayActionsActivity(Player activePlayer, int actions)
            : base(null, activePlayer, string.Format("Play actions. You have {0} action(s) remaining.", actions), ActivityType.PlayActions)
        {
        }
    }
}