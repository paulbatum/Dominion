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
            var items = new Dictionary<string, int>
            {
                {"Moat", 2}
            };

            var result = items.GetCumulativeProbabilities();
            CollectionAssert.AreEquivalent(items, result);
        }

        [Test]
        public void CumulativeTotalWithTwoItems()
        {
            var items = new Dictionary<string, int>
            {
                {"Moat", 2},
                {"Smithy", 3}
            };

            var result = items.GetCumulativeProbabilities();
            var expected = new Dictionary<string, int>
            {
                {"Moat", 2},
                {"Smithy", 5}
            };

            CollectionAssert.AreEquivalent(expected, result);

        }

    }
}
