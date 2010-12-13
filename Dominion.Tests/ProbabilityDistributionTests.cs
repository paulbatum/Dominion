using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.GameHost.AI.BehaviourBased;
using NUnit.Framework;

namespace Dominion.Tests
{
    [TestFixture]
    public class ProbabilityDistributionTests
    {
        [Test]
        public void CumulativeTotalBaseCase()
        {
            var items = new Dictionary<string, int>();

            var result = items.GetCumulativeTotals();
            CollectionAssert.AreEquivalent(items, result);
        }

        [Test]
        public void CumulativeTotalWithOneItem()
        {
            var items = new Dictionary<string, int>
            {
                {"Moat", 2}
            };

            var result = items.GetCumulativeTotals();
            CollectionAssert.AreEquivalent(items, result);
        }

        [Test]
        public void CumulativeTotalWithThreeItems()
        {
            var items = new Dictionary<string, int>
            {
                {"Moat", 2},
                {"Smithy", 3},
                {"Village", 1}
            };

            var result = items.GetCumulativeTotals();
            var expected = new Dictionary<string, int>
            {
                {"Moat", 2},
                {"Smithy", 5},
                {"Village", 6}
            };

            CollectionAssert.AreEquivalent(expected, result);

        }

        [Test]
        public void GetRandomItemReturnsEquallyLikelyItemsWithEqualProbability()
        {
            var items = new[] {"Moat", "Smithy", "Village", "Market"};
            var randomNumbers = new[] {0, 1, 2, 3};
            
            var distribution = new ProbabilityDistribution(new RandomNumberProviderStub(randomNumbers), items);
            var occurances = new Dictionary<string, int>();

            foreach (var item in items)
                occurances[item] = 0;

            for (int i = 0; i < randomNumbers.Length; i++)
                occurances[distribution.RandomItem(items)]++;

            var expected = new Dictionary<string, int>
            {
                {"Moat", 1},
                {"Smithy", 1},
                {"Village", 1},
                {"Market", 1}
            };

            CollectionAssert.AreEquivalent(expected, occurances);
        }

        [Test]
        public void GetRandomItemReturnsUnequallyProbableItemsWithExpectedProbability()
        {
            var items = new[] { "Moat", "Smithy", "Village", "Market" };
            var randomNumbers = new[] { 0, 1, 2, 3, 4 };

            var distribution = new ProbabilityDistribution(new RandomNumberProviderStub(randomNumbers), items);
            distribution.IncreaseLikelihood("Village");
            var occurances = new Dictionary<string, int>();

            foreach (var item in items)
                occurances[item] = 0;

            for (int i = 0; i < randomNumbers.Length; i++)
                occurances[distribution.RandomItem(items)]++;

            var expected = new Dictionary<string, int>
            {
                {"Moat", 1},
                {"Smithy", 1},
                {"Village", 2},
                {"Market", 1}
            };

            CollectionAssert.AreEquivalent(expected, occurances);
        }

        [Test]
        public void GetRandomItemReturnsFilteredItemsWithExpectedProbability()
        {
            var items = new[] { "Moat", "Smithy", "Village", "Market" };
            var randomNumbers = new[] { 0, 1, 2, 3, 4 };

            var distribution = new ProbabilityDistribution(new RandomNumberProviderStub(randomNumbers), items);
            distribution.IncreaseLikelihood("Village");
            var occurances = new Dictionary<string, int>();

            foreach (var item in items)
                occurances[item] = 0;

            for (int i = 0; i < randomNumbers.Length; i++)
                occurances[distribution.RandomItem(items)]++;

            var expected = new Dictionary<string, int>
            {
                {"Moat", 1},
                {"Smithy", 1},
                {"Village", 2},
                {"Market", 1}
            };

            CollectionAssert.AreEquivalent(expected, occurances);
        }
    }

    public class RandomNumberProviderStub : IRandomNumberProvider
    {
        private IEnumerator<int> _enumerator;

        public RandomNumberProviderStub(params int[] values)
        {             
            _enumerator = values.Cast<int>().GetEnumerator();
        }

        public int Next(int inclusiveLowerBound, int exclusiveUpperBound)
        {
            _enumerator.MoveNext();
            return _enumerator.Current;
        }
    }
}
