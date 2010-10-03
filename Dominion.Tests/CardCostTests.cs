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
        public void CardCost_3P_minus_CardCost_2P_equals_1()
        {
            CardCost cost3P = new CardCost(3, 1);
            CardCost cost2P = new CardCost(2, 1);
            var newCost = cost3P - cost2P;
            newCost.Money.ShouldEqual(1);
            newCost.Potions.ShouldEqual(0);
        }


        [Test]
        public void CardCost_of_5_is_enough_for_CardCost_of_5()
        {
            CardCost cost1 = 5;
            CardCost cost2 = 5;

            Assert.That(cost1.IsEnoughFor(cost2));
        }


        [Test]
        public void CardCost_of_2P_is_enough_for_CardCost_of_2P()
        {
            CardCost cost1 = CardCost.Parse("2P");
            CardCost cost2 = CardCost.Parse("2P");

            Assert.That(cost1.IsEnoughFor(cost2));
        }

        [Test]
        public void CardCost_of_5_is_enough_for_CardCost_of_3()
        {
            CardCost cost5 = 5;
            CardCost cost3 = 3;            

            Assert.That(cost5.IsEnoughFor(cost3));
        }

        [Test]
        public void CardCost_of_5_is_not_enough_for_CardCost_of_3P()
        {
            CardCost cost5 = 5;
            CardCost cost3P = CardCost.Parse("3P");            

            Assert.False(cost5.IsEnoughFor(cost3P));
        }

        [Test]
        public void CardCost_of_3P_is_not_enough_for_CardCost_of_5()
        {
            CardCost cost5 = 5;
            CardCost cost3P = CardCost.Parse("3P");

            Assert.False(cost3P.IsEnoughFor(cost5));
        }
        

        [Test]
        public void Can_compare_CardCost_to_integer()
        {
            CardCost cost = 5;
            int otherCost = 5;

            Assert.That( cost == otherCost);       
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
