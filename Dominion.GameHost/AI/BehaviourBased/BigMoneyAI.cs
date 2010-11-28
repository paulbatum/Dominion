namespace Dominion.GameHost.AI.BehaviourBased
{
    public class BigMoneyAI : BehaviourBasedAI
    {
        public BigMoneyAI()
        {
            Behaviours.Add(new CommentOnGameEndBehaviour("The game is over? But I was about to buy a Province!"));

            Behaviours.Add(new DefaultDiscardCardsBehaviour());
            Behaviours.Add(new DefaultMakeChoiceBehaviour());
            Behaviours.Add(new DefaultPassCardsBehaviour());
            Behaviours.Add(new DefaultSelectFromRevealedBehaviour());

            Behaviours.Add(new BuyPointsBehaviour(6));
            Behaviours.Add(new BigMoneyBuyBehaviour());

            Behaviours.Add(new SkipActionsBehaviour());
        }
    }

    public class SimpleAI : BehaviourBasedAI
    {
        public SimpleAI()
        {
            Behaviours.Add(new CommentOnGameEndBehaviour("The game is over? But I was about to buy a Province!"));

            Behaviours.Add(new DefaultDiscardCardsBehaviour());
            Behaviours.Add(new DefaultMakeChoiceBehaviour());
            Behaviours.Add(new DefaultPassCardsBehaviour());
            Behaviours.Add(new DefaultSelectFromRevealedBehaviour());

            Behaviours.Add(new BuyPointsBehaviour(6));
            Behaviours.Add(new BuyAlternatingMoneyAndActionsBehaviour());

            Behaviours.Add(new PlaySimpleActionsBehaviour());
        }
    }
}