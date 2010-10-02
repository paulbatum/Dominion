namespace Dominion.Rules.CardTypes
{
    public interface ITreasureCard : ICard
    {
        CardCost Value { get; }
    }
}