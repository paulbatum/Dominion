namespace Dominion.GameHost.AI.BehaviourBased
{
    public class ProbabilityAI : BehaviourBasedAI
    {
        public ProbabilityAI()
        {
            var buyBehaviour = new ProbabilisticBuyBehaviour();

            Behaviours.Add(new ProbabilisticBuyBehaviour.LearnFromGameResultBehaviour(buyBehaviour));

            Behaviours.Add(new DefaultDiscardOrRedrawCardsBehaviour());
            Behaviours.Add(new DefaultMakeChoiceBehaviour());
            Behaviours.Add(new DefaultSelectFixedNumberOfCardsToPassOrTrashBehaviour());
            Behaviours.Add(new DefaultSelectFromRevealedBehaviour());
            Behaviours.Add(new DefaultSelectFixedNumberOfCardsForPlayBehaviour());
            Behaviours.Add(new DefaultSelectUpToNumberOfCardsToTrashBehaviour());

            Behaviours.Add(new PlaySimpleActionsBehaviour());
            Behaviours.Add(new SkipActionsBehaviour());

            Behaviours.Add(new BuyPointsBehaviour(6));
            Behaviours.Add(buyBehaviour);
            Behaviours.Add(new SkipBuyBehaviour());


        }
    }
}