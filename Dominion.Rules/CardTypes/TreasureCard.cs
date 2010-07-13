namespace Dominion.Rules.CardTypes
{
    public abstract class TreasureCard : Card
    {
        public override bool CanPlay(TurnContext context)
        {
            return true;
        }
    }
}