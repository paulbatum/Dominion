namespace Dominion.Rules.CardTypes
{
    public interface IActionCard : ICard
    {
        void Play(TurnContext context);
    }

    public interface IReactionCard : ICard
    {
        
    }

    public interface IAttackCard : ICard
    {
        
    }
}