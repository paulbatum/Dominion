namespace Dominion.Rules.CardTypes
{
    public interface IActionCard : ICard
    {

        void Play(TurnContext context);
    }
}