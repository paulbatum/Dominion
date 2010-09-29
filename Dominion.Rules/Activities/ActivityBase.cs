using System;
using System.Collections;
using System.Linq;
using Dominion.Rules.CardTypes;
using System.Collections.Generic;

namespace Dominion.Rules.Activities
{
    public abstract class ActivityBase : IActivity
    {
        protected IGameLog Log { get; private set; }
        public Player Player { get; private set; }
        public string Message { get; private set; }
        public ActivityType Type { get; private set; }
        public ActivityCategory Category { get; protected set; }
        public Guid Id { get; private set; }

        public bool IsSatisfied { get; protected set; }

        public virtual IDictionary<string, object> Properties
        {
            get { return new Dictionary<string, object>();}
        }

        protected ActivityBase(IGameLog log, Player player, string message, ActivityType type)
        {
            Log = log;
            Player = player;
            Message = message;
            Type = type;
            Category = ActivityCategory.None;
            Id = Guid.NewGuid();
        }
    }

    public enum ActivityCategory
    {
        None,
        SelectFromHand,
        SelectFromRevealed
    }

    public enum ActivityType
    {
        SelectFixedNumberOfCards,
        WaitingForOtherPlayers,
        SelectPile,
        MakeChoice,
        SelectUpToNumberOfCards
    }
}
