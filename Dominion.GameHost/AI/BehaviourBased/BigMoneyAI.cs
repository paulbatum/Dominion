namespace Dominion.GameHost.AI.BehaviourBased
{
    public class BigMoneyAI : BehaviourBasedAI
    {
        public BigMoneyAI()
        {
            Behaviours.Add(new CommentOnGameEndBehaviour("The game is over? But I was about to buy a Province!"));

            Behaviours.Add(new DefaultDiscardOrRedrawCardsBehaviour());
            Behaviours.Add(new DefaultMakeChoiceBehaviour());
            Behaviours.Add(new DefaultSelectFixedNumberOfCardsToPassOrTrashBehaviour());
            Behaviours.Add(new DefaultSelectFromRevealedBehaviour());
            Behaviours.Add(new DefaultSelectFixedNumberOfCardsForPlayBehaviour());            
            Behaviours.Add(new DefaultSelectUpToNumberOfCardsToTrashBehaviour());

            Behaviours.Add(new SkipActionsBehaviour());

            Behaviours.Add(new BuyPointsBehaviour(6));
            Behaviours.Add(new BigMoneyBuyBehaviour());
            Behaviours.Add(new SkipBuyBehaviour());

        }
    }
}