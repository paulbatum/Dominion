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
        public Guid Id { get; private set; }

        public bool IsSatisfied { get; protected set; }

        public virtual void WriteProperties(IDictionary<string, object> bag)
        {
            
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
        SelectFixedNumberOfCards,
        WaitingForOtherPlayers,
        SelectPile,
        SelectAnyNumberOfCards,
        MakeChoice
    }

    public enum RestrictionType
    {
        ActionCard,
        ReactionCard,
        TreasureCard
    }
}