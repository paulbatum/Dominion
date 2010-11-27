using System;
using System.Collections.Generic;

namespace Dominion.Rules.Activities
{
    public interface ISelectPileActivity : IActivity
    {
        void SelectPile(CardPile pile);
        ISelectionSpecification Specification { get; }
    }

    public class SelectPileActivity : ActivityBase, ISelectPileActivity
    {
        public SelectPileActivity(IGameLog log, Player player, string message, ISelectionSpecification specification, ICard source)
            : base(log, player, message, specification.ActivityType, source)
        {
            Specification = specification;
        }

        public ISelectionSpecification Specification { get; private set; }
        public Action<CardPile> AfterPileSelected { get; set; }

        protected void CheckPile(CardPile pile)
        {
            if (!Specification.IsMatch(pile))
                throw new ArgumentException("Pile does not match specification!", "pile");
        }
        
        public virtual void SelectPile(CardPile pile)
        {
            CheckPile(pile);
            AfterPileSelected(pile);
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