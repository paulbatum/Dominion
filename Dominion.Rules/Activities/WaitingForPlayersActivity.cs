namespace Dominion.Rules.Activities
{
    public class WaitingForPlayersActivity : ActivityBase
    {
        public WaitingForPlayersActivity(Player waitingPlayer) 
            : base(null, waitingPlayer, "Waiting for other players...", ActivityType.WaitingForOtherPlayers, null)
        {
        }
    }
}