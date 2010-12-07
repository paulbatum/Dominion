using System;
using System.Collections.Generic;
using System.Linq;

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

    public class ProbabilisticBuyBehaviour : BuyBehaviourBase
    {
        private ProbabilityDistribution _distribution;

        public ProbabilisticBuyBehaviour()
        {
            _distribution = new ProbabilityDistribution(AISupportedActions.All.Concat(Treasure.Basic));
        }

        public override bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return base.CanRespond(activity, state) && GetValidBuys(state)
                .Any(c => _distribution.Contains(c.Name));
        }

        protected override CardPileViewModel SelectPile(GameViewModel state)
        {
            throw new NotImplementedException();
        }

        
    }

    public class ProbabilityDistribution
    {
        private Dictionary<string, int> _probabilities;
        private Random _random;

        public ProbabilityDistribution(IEnumerable<string> items)
        {
            _random = new Random();
            _probabilities = new Dictionary<string, int>();
            foreach (string item in items)
                _probabilities[item] = 1;
        }

        public bool Contains(string item)
        {
            return _probabilities.ContainsKey(item);
        }

        


        public string RandomItem(IEnumerable<string> options)
        {
            var validProbabilities = _probabilities.Where(kvp => options.Contains(kvp.Key)).ToList();
            var upperBound = validProbabilities
                .Sum(kvp => kvp.Value);

            var cumulativeProbabilities = validProbabilities.GetCumulativeProbabilities();

            var number = _random.Next(0, upperBound);

            return cumulativeProbabilities.SkipWhile(p => number > p.Value).First().Key;
        }

    }

    public static class ProbabilityExtensions
    {
        public static IEnumerable<KeyValuePair<T, int>> GetCumulativeProbabilities<T>(IEnumerable<KeyValuePair<T, int>> other, KeyValuePair<T, int> current, int prevValue)
        {
            //var temp = 0;
            //foreach (var item in input)
            //{
            //    yield return new KeyValuePair<T, int>(item.Key, item.Value + temp);
            //    temp += item.Value;
            //}

            var value = current.Value + prevValue;
            var cumulativeItem = new[] { new KeyValuePair<T, int>(current.Key, value) };

            if (other.Any())
            {
                return cumulativeItem.Concat(GetCumulativeProbabilities(other.Skip(1), other.First(), value));
            }

            return cumulativeItem;
        }

        public static IEnumerable<KeyValuePair<T, int>> GetCumulativeProbabilities<T>(this IEnumerable<KeyValuePair<T, int>> items)
        {
            return GetCumulativeProbabilities(items.Skip(1), items.First(), 0);
        }
    }

}