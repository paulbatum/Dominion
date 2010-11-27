using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.Activities;

namespace Dominion.Rules
{
    public interface ICardEffect
    {        
        bool HasFinished { get; }
        ICard Source { get; set; }
        IActivity GetActivity(Player player);
        void BeginResolve(TurnContext currentTurn);
    }

    public abstract class CardEffectBase : ICardEffect
    {        
        protected readonly IList<IActivity> _activities = new List<IActivity>();
        private bool _isResolved = false;

        public abstract void Resolve(TurnContext context, ICard source);

        public ICard Source { get; set; }

        public bool HasFinished
        {
            get
            {
                return _isResolved && _activities.All(a => a.IsSatisfied);
            }
        }

        public virtual IActivity GetActivity(Player player)
        {
            var activity = _activities.FirstOrDefault(a => a.Player == player && !a.IsSatisfied);
            if (activity != null)
                return activity;

            if (_activities.Any())
                return new WaitingForPlayersActivity(player);

            return null;
        }

        public virtual void BeginResolve(TurnContext currentTurn)
        {
            if (!_isResolved)
            {
                _isResolved = true;
                Resolve(currentTurn, Source);
            }
        }
    }

}