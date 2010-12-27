using System;
using System.Collections.Generic;

namespace Dominion.Rules.Activities
{
    public interface ISelectPileActivity : IActivity
    {
        void SelectPile(CardPile pile);
        ISelectionSpecification Specification { get; }
        bool IsOptional { get; set; }
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
        public bool IsOptional { get; set; }

        protected void CheckPile(CardPile pile)
        {            
            if (!Specification.IsMatch(pile))
                throw new ArgumentException("Pile does not match specification!", "pile");
        }
        
        public virtual void SelectPile(CardPile pile)
        {
            if (pile != null)
            {
                CheckPile(pile);
                AfterPileSelected(pile);
            }
            else
            {
                if (IsOptional == false)
                    throw new InvalidOperationException("No pile selected, but activity is not optional!");
            }

            IsSatisfied = true;
        }

        public override IDictionary<string, object> Properties
        {
            get
            {
                var properties = base.Properties;
                Specification.WriteProperties(properties);
                properties["IsOptional"] = IsOptional;
                return properties;
            }
        }

    }
}