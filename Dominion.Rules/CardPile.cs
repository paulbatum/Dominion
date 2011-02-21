using System;

namespace Dominion.Rules
{
    public abstract class CardPile : CardZone
    {
        public Guid Id { get; set; }
        public abstract bool IsEmpty { get; }
        public abstract ICard TopCard { get; }
        public abstract bool IsLimited { get; }

        private string _cachedName;

        public string Name
        {
            get
            {
                // The pile could run out, lets remember the name.
                if (_cachedName == null)
                    _cachedName = TopCard.Name;

                return _cachedName;
            }
        }

        public CardPile()
        {
            Id = Guid.NewGuid();
        }        
    }
}