namespace Dominion.Rules.Activities
{
    public class GainACardActivity : ActivityBase
    {
        public GainACardActivity(IGameLog log, Player player, string message, ActivityType type) 
            : base(log, player, message, type)
        {
        }        
    }    
}