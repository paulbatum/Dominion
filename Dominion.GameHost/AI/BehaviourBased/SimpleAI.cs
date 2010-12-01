namespace Dominion.GameHost.AI.BehaviourBased
{
    public class SimpleAI : BehaviourBasedAI
    {
        public SimpleAI()
        {
            Behaviours.Add(new CommentOnGameEndBehaviour("The game is over? But I was about to buy a Province!"));

            Behaviours.Add(new DefaultDiscardOrRedrawCardsBehaviour());
            Behaviours.Add(new DefaultMakeChoiceBehaviour());
            Behaviours.Add(new DefaultSelectFixedNumberOfCardsToPassOrTrashBehaviour());
            Behaviours.Add(new DefaultSelectFromRevealedBehaviour());
            Behaviours.Add(new DefaultSelectFixedNumberOfCardsForPlayBehaviour());
            Behaviours.Add(new DefaultSelectUpToNumberOfCardsToTrashBehaviour());

            Behaviours.Add(new BuyPointsBehaviour(6));
            Behaviours.Add(new BuyAlternatingMoneyAndActionsBehaviour());

            Behaviours.Add(new PlaySimpleActionsBehaviour());
            Behaviours.Add(new SkipActionsBehaviour());
        }
    }
}