using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.Activities;

namespace Dominion.Rules
{
    public interface ICardEffect
    {
        bool HasFinished { get; }
        IActivity GetActivity(Player player);
    }

    public abstract class CardEffectBase : ICardEffect
    {
        protected readonly IList<IActivity> _activities = new List<IActivity>();

        public bool HasFinished
        {
            get
            {
                return _activities.All(a => a.IsSatisfied);
            }
        }

        public IActivity GetActivity(Player player)
        {
            var activity = _activities.FirstOrDefault(a => a.Player == player && !a.IsSatisfied);
            if (activity != null)
                return activity;

            if (_activities.Any())
                return new WaitingForPlayersActivity(player);

            return null;
        }
    }
}