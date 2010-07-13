namespace Dominion.Rules.CardTypes
{
    public abstract class MoneyCard : TreasureCard
    {
        protected MoneyCard(int value)
        {
            Value = value;
        }

        public int Value { get; protected set; }

        public override void Play(TurnContext context)
        {
            context.MoneyToSpend += Value;
        }
    }
}