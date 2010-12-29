using System.Linq;
using Dominion.Cards.Victory;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class BuyPointsBehaviour : BuyBehaviourBase
    {
        private readonly int _supplyThresholdForPointsBuyingMode;

        public BuyPointsBehaviour(int supplyThresholdForPointsBuyingMode)
        {
            _supplyThresholdForPointsBuyingMode = supplyThresholdForPointsBuyingMode;
        }

        public override bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return base.CanRespond(activity, state) && BuyingPointsIsWorthIt(state);
        }

        protected virtual bool BuyingPointsIsWorthIt(GameViewModel state)
        {
            var gameEndingPiles = state.Bank.Where(pile => pile.Is<Colony>() || pile.Is<Province>());
            
            bool anyGameEndingPilesRunningLow = gameEndingPiles.Any(pile => pile.Count <= _supplyThresholdForPointsBuyingMode);
            bool severalPilesAreEmpty = state.Bank.Count(pile => pile.IsLimited && pile.Count == 0) >= 2;
            bool closeToEnding = anyGameEndingPilesRunningLow || severalPilesAreEmpty;

            var bestBuy = GetValidBuys(state)
                .OrderByDescending(pile => pile.Cost)
                .First();

            bool bestBuyIsFromGameEndingPile = gameEndingPiles.Contains(bestBuy);

            if(closeToEnding || bestBuyIsFromGameEndingPile)
            {
                return GetValidBuys(state)
                    .Where(pile => pile.Is(CardType.Victory))
                    .Any();
            }

            return false;
        }

        protected override CardPileViewModel SelectPile(GameViewModel state, IGameClient client)
        {
            return GetValidBuys(state)
                .Where(pile => pile.Is(CardType.Victory))
                .OrderByDescending(pile => VictoryCards.Basic.Contains(pile.Name))
                .First();
        }

        protected override void TalkSmack(CardPileViewModel pile, IGameClient client)
        {
            base.TalkSmack(pile, client);

            if (pile.Name == "Province")
                client.SendChatMessage("Province muthafucka!");

            if (pile.Name == "Colony")
                client.SendChatMessage("COLONY! SUCK IT!");
        }
    }
}