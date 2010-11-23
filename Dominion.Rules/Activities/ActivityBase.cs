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
        public ActivityType Type { get; protected set; }
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
            Id = Guid.NewGuid();
        }
    }

    public enum ActivityType
    {
        SelectFromRevealed,
        SelectFixedNumberOfCards,
        WaitingForOtherPlayers,
        SelectPile,
        MakeChoice,
        SelectUpToNumberOfCards,
        DoBuys,
        PlayActions
    }
}
