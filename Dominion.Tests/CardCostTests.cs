using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using NUnit.Framework;

namespace Dominion.Tests
{
    [TestFixture]
    public class CardCostTests
    {
        [Test]
        public void Can_convert_integer_to_CardCost()
        {
            CardCost cost = 5;
            cost.Money.ShouldEqual(5);
            cost.Potions.ShouldEqual(0);
        }

        [Test]
        public void Can_create_CardCost_by_adding_to_existing_CardCost()
        {
            CardCost cost = new CardCost(5);
            cost += 2;
            cost.Money.ShouldEqual(7);
            cost.Potions.ShouldEqual(0);
        }

        [Test]
        public void Can_create_CardCost_by_subtracting_from_existing_CardCost()
        {
            CardCost cost = new CardCost(5);
            var newCost = cost - 2;
            newCost.Money.ShouldEqual(3);
            newCost.Potions.ShouldEqual(0);
        }

        [Test]
        public void CardCost_of_3_is_less_than_CardCost_of_5()
        {
            CardCost cost3 = 3;
            CardCost cost5 = 5;

            Assert.That(cost3 < cost5);
        }

        [Test]
        public void CardCost_of_5_is_greater_than_CardCost_of_3()
        {
            CardCost cost3 = 3;
            CardCost cost5 = 5;

            Assert.That(cost5 > cost3);
        }

        [Test]
        public void CardCost_of_5_is_greater_than_or_equal_to_CardCost_of_5()
        {
            CardCost c1 = 5;
            CardCost c2 = 5;

            Assert.That(c1 >= c2);
        }

        [Test]
        public void Can_compare_CardCost_to_integer()
        {
            CardCost cost = 5;
            int otherCost = 5;

            Assert.That( cost == otherCost);       
        }

        [Test]
        public void Cannot_use_magnitude_comparison_between_CardCosts_with_differing_potions()
        {
            CardCost costWithPotion = new CardCost(3, 1);
            CardCost costWithNoPotion = 5;

            Assert.Throws(typeof (ArgumentException), () => { bool x = costWithNoPotion > costWithPotion; });
        }

        [Test]
        public void CardCosts_with_same_values_are_equal()
        {
            CardCost cost1 = new CardCost(3, 1);
            CardCost cost2 = new CardCost(3, 1);

            Assert.AreEqual(cost1, cost2);
            Assert.That(cost1 == cost2);
        }

        [Test]
        public void Null_CardCosts_are_equal()
        {
            CardCost cost1 = null;
            CardCost cost2 = null;

            Assert.AreEqual(cost1, cost2);
            Assert.That(cost1 == cost2);
        }

        [Test]
        public void Can_parse_string_as_CardCost()
        {
            string stringCost = "3";
            CardCost cost = CardCost.Parse(stringCost);

            cost.Money.ShouldEqual(3);
            cost.Potions.ShouldEqual(0);
        }

        [Test]
        public void Can_parse_string_with_potions_as_CardCost()
        {
            string stringCost = "3PP";
            CardCost cost = CardCost.Parse(stringCost);

            cost.Money.ShouldEqual(3);
            cost.Potions.ShouldEqual(2);
        }

        [Test]
        public void Can_explicitly_convert_from_string_to_CardCost()
        {
            string stringCost = "3P";
            CardCost cost = (CardCost)stringCost;

            cost.Money.ShouldEqual(3);
            cost.Potions.ShouldEqual(1);
        }
    }
}
