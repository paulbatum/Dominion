using System.Collections.Generic;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class BuyAlternatingMoneyAndActionsBehaviour : IAIBehaviour
    {
        private readonly BuySimpleActionsBehaviour _buySimpleAction = new BuySimpleActionsBehaviour();
        private readonly BigMoneyBuyBehaviour _bigMoney = new BigMoneyBuyBehaviour();
        private readonly IEnumerator<IAIBehaviour> _enumerator;

        public BuyAlternatingMoneyAndActionsBehaviour()
        {
            _enumerator = Alternating().GetEnumerator();
            _enumerator.MoveNext();
        }

        private IEnumerable<IAIBehaviour> Alternating()
        {            
            yield return new BuyPotionBehaviour();

            while (true)
            {                
                yield return _buySimpleAction;
                yield return _bigMoney;
                yield return _bigMoney;
            }
        }

        public bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return _buySimpleAction.CanRespond(activity, state) || _bigMoney.CanRespond(activity, state);
        }

        public void Respond(IGameClient client, ActivityModel activity, GameViewModel state)
        {
            while (!_enumerator.Current.CanRespond(activity, state))
                _enumerator.MoveNext();

            _enumerator.Current.Respond(client, activity, state);
            _enumerator.MoveNext();
        }
    }
}