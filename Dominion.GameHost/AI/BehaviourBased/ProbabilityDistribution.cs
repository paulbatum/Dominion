using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class ProbabilityDistribution
    {
        private Dictionary<string, int> _probabilities;
        private IRandomNumberProvider _random;

        public ProbabilityDistribution(params IEnumerable<string>[] items) : this(new RandomNumberProvider(), items)
        {
            
        }

        public ProbabilityDistribution(IRandomNumberProvider random, params IEnumerable<string>[] items)
        {
            _random = random;
            _probabilities = new Dictionary<string, int>();
            foreach (string item in items.SelectMany(x => x))
                _probabilities[item] = 1;
        }

        public bool Contains(string item)
        {
            return _probabilities.ContainsKey(item);
        }


        public void IncreaseLikelihood(string item)
        {
            lock(_probabilities)
                _probabilities[item]++;
        }


        public string RandomItem(IEnumerable<string> options)
        {
            List<KeyValuePair<string, int>> validProbabilities;
            lock (_probabilities)
            {
                validProbabilities = _probabilities.Where(kvp => options.Contains(kvp.Key)).ToList();
            }

            var upperBound = validProbabilities
                .Sum(kvp => kvp.Value);

            var cumulativeProbabilities = validProbabilities.GetCumulativeTotals();

            var number = _random.Next(0, upperBound);

            return cumulativeProbabilities.SkipWhile(p => number >= p.Value).First().Key;
        }

        public T RandomItem<T>(IEnumerable<T> items, Func<T, string> selector)
        {
            var item = RandomItem(items.Select(selector));
            return items.Single(i => selector(i) == item);
        }

        public override string ToString()
        {
            lock (_probabilities)
            {
                var builder = new StringBuilder();
                foreach (var kvp in _probabilities)
                {
                    builder.AppendFormat("{0} - {1}", kvp.Key, kvp.Value).AppendLine();
                }
                return builder.ToString();
            }
        }

    }

    public class RandomNumberProvider : IRandomNumberProvider
    {
        private readonly Random _random = new Random();

        public int Next(int inclusiveLowerBound, int exclusiveUpperBound)
        {
            return _random.Next(inclusiveLowerBound, exclusiveUpperBound);
        }
    }

    public interface IRandomNumberProvider
    {
        int Next(int inclusiveLowerBound, int exclusiveUpperBound);
    }
}