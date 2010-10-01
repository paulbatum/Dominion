using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules.Activities
{
    public interface ISelectCardsActivity : IActivity
    {        
        void SelectCards(IEnumerable<ICard> cards);                
    }

    public class SelectCardsActivity : ActivityBase, ISelectCardsActivity
    {
        public SelectCardsActivity(IGameLog log, Player player, string message, ISelectionSpecification specification) 
            : base(log, player, message, specification.ActivityType)
        {
            Specification = specification;
        }

        public SelectCardsActivity(TurnContext context, string message, ISelectionSpecification specification)
            : this(context.Game.Log, context.ActivePlayer, message, specification)
        {
            
        }

        public ISelectionSpecification Specification { get; private set; }
        public Action<IEnumerable<ICard>> AfterCardsSelected { get; set; }

        protected void CheckCards(IEnumerable<ICard> cards)
        {
            if (!Specification.IsMatch(cards))
                throw new ArgumentException("Selected cards do not match specification!", "cards");
        }

        public virtual void SelectCards(IEnumerable<ICard> cards)
        {
            CheckCards(cards);
            AfterCardsSelected(cards.ToList());
            IsSatisfied = true;
        }

        public override IDictionary<string, object> Properties
        {
            get
            {
                var properties = base.Properties;
                Specification.WriteProperties(properties);
                return properties;
            }
        }
    }


}