using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Curses
{
    public class Curse : Card, ICurseCard
    {
        public Curse() : base(0) { }

        public int Score(CardZone allCards)
        {
            return -1;
        }
    }
}