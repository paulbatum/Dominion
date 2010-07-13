using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Cards.Treasure;
using Dominion.Rules;
using NUnit.Framework;

namespace Dominion.Tests
{
    [TestFixture]
    public class CardMovementTests
    {
        private CardZone _zone1;
        private CardZone _zone2;
        private Card _card;

        [SetUp]
        public void SetUp()
        {
            _zone1 = new CardZone();
            _zone2 = new CardZone();
            _card = new Copper();
        }

        [Test]
        public void Card_should_start_in_null_zone()
        {
            _card.CurrentZone.ShouldBeOfType<NullZone>();
        }

        [Test]
        public void Card_should_change_current_zone_when_moved()
        {
            _card.MoveTo(_zone1);
            _card.CurrentZone.ShouldEqual(_zone1);
        }

        [Test]
        public void Card_should_be_added_to_target_zone_when_moved()
        {
            _card.MoveTo(_zone1);
            _zone1.CardCount.ShouldEqual(1);
        }

        [Test]
        public void Card_should_be_removed_from_old_zone_when_moved()
        {
            _card.MoveTo(_zone1);
            _card.MoveTo(_zone2);
            _zone1.CardCount.ShouldEqual(0);
            _zone2.CardCount.ShouldEqual(1);
        }
    }
}
