namespace Dominion.Rules.CardTypes
{
    public abstract class CurseCard : Card
    {
        public override bool CanPlay(TurnContext context)
        {
            return false;
        }
    }
}