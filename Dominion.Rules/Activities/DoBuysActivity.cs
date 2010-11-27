namespace Dominion.Rules.Activities
{
    public class DoBuysActivity : ActivityBase
    {
        public DoBuysActivity(Player activePlayer, int buys, CardCost buyValue)
            : base(null, activePlayer, string.Format("Select card(s) to buy. You have {0} buy(s) for a total of {1}.", buys, buyValue), ActivityType.DoBuys, null)
        {
        }
    }
}