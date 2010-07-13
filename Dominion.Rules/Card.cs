using System;

namespace Dominion.Rules
{
    public delegate void CardZoneChanger(CardZone targetZone);

    public abstract class Card
    {
        private CardZone _currentZone;
        private readonly CardZoneChanger _zoneChanger;

        protected Card()
        {
            _currentZone = new NullZone();
            _zoneChanger = zone => _currentZone = zone;
        }

        public void MoveTo(CardZone targetZone)
        {
            _currentZone.MoveCard(this, targetZone, _zoneChanger);            
        }

        public CardZone CurrentZone
        {
            get { return _currentZone; }
        }

        public abstract bool CanPlay(TurnContext context);
        public abstract void Play(TurnContext context);

        public virtual int Score(DrawDeck deck)
        {
            return 0;
        }
    }    

    public class NullZone : CardZone
    {
        protected override void AddCard(Card card)
        {
            // NO OP
        }

        protected override void RemoveCard(Card card)
        {
            // NO OP
        }
    }
}