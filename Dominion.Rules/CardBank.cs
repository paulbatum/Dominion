using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules
{
    public class CardBank
    {
        private IList<CardPile> _piles;
        private IList<CardPile> _gameEndPiles;

        public CardBank()
        {
            _piles = new List<CardPile>();
            _gameEndPiles = new List<CardPile>();
        }

        public IEnumerable<CardPile> Piles
        {
            get { return _piles; }
        }

        public void AddCardPile(CardPile pile)
        {
            _piles.Add(pile);
        }

        public void AddCardPileWhichEndsTheGameWhenEmpty(CardPile pile)
        {
            AddCardPile(pile);
            _gameEndPiles.Add(pile);
        }

        public int EmptyPileCount
        {
            get
            {
                return _piles.Count(x => x.IsEmpty);
            }
        }

        public int EmptyGameEndingPilesCount
        {
            get { return _gameEndPiles.Count(x => x.IsEmpty); }
        }

        public CardPile Pile<T>()
        {
            return this.Piles.Single(x => x.Name == typeof(T).Name);
        }

        public CardPile NonEmptyPile<T>()
        {
            var pile = this.Pile<T>();
            return pile.IsEmpty ? null : pile;
        }
    }

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

    public class LimitedSupplyCardPile : CardPile
    {
        public override bool IsEmpty
        {
            get { return this.CardCount == 0; }
        }

        public override ICard TopCard
        {
            get { return Cards.FirstOrDefault(); }
        }

        public override bool IsLimited
        {
            get { return true; }
        }
    }

    public class UnlimitedSupplyCardPile : CardPile
    {
        private readonly Func<ICard> _cardCreator;

        public UnlimitedSupplyCardPile(Func<ICard> cardCreator)
        {
            _cardCreator = cardCreator;
        }

        public override bool IsEmpty
        {
            get { return false; }
        }

        public override int CardCount
        {
            get
            {
                throw new NotSupportedException("A null zone cannot be considered to have a card count");                
            }
        }

        public override ICard TopCard
        {
            get { return _cardCreator(); }
        }

        public override bool IsLimited
        {
            get { return false; }
        }

        protected override void AddCard(ICard card)
        {
            // NO OP
        }

        protected override void RemoveCard(ICard card)
        {
            // NO OP
        }
        
    }
}