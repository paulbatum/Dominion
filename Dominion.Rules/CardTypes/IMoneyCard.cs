namespace Dominion.Rules.CardTypes
{
    public interface IMoneyCard : ITreasureCard, ICard
    {
        int Value { get; }
    }
}