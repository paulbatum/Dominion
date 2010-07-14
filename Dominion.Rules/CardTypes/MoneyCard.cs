namespace Dominion.Rules.CardTypes
{
    public abstract class MoneyCard : TreasureCard
    {
        protected MoneyCard(int value, int cost) : base(cost)
        {
            Value = value;
        }

        public int Value { get; protected set; }
    }
}