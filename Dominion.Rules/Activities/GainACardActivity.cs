namespace Dominion.Rules.Activities
{
    public class GainACardActivity : ActivityBase
    {
        public GainACardActivity(IGameLog log, Player player, string message, ActivityType type) 
            : base(log, player, message, type)
        {
        }

        public virtual void SelectPileToGainFrom(CardPile pile)
        {
            var card = pile.TopCard;
            card.MoveTo(Player.Discards);
            Log.LogGain(Player, card);
            IsSatisfied = true;
        }
    }
}