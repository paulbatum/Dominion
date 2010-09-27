namespace Dominion.Rules.CardTypes
{
    public interface IActionCard : ICard
    {
        void Play(TurnContext context);
    }

    public interface IReactionCard : ICard
    {
        void React(AttackEffect attackEffect, Player player, TurnContext currentTurn);
        
        /// <summary>
        /// Does it make sense for the player's reaction window to stay open if this was the only reaction in their hand? 
        /// For example, if you only have a moat in your hand, once its revealed, there is no point in keeping the reaction window open. The rules say you can, but there is no point.
        /// Secret Chamber on the other hand, might draw you another reaction, so its worth keeping the window open.
        /// </summary>
        bool ContinueReactingIfOnlyReaction { get; }
    }

    public interface IAttackCard : ICard
    {
        
    }
}