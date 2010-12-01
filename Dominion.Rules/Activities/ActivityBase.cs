using System;
using System.Collections;
using System.Linq;
using Dominion.Rules.CardTypes;
using System.Collections.Generic;

namespace Dominion.Rules.Activities
{
    public abstract class ActivityBase : IActivity
    {
        public Guid Id { get; private set; }      
        protected IGameLog Log { get; private set; }
        public Player Player { get; private set; }
        public string Message { get; private set; }
        public string Source { get; private set; }
        public ActivityType Type { get; protected set; }          
        public bool IsSatisfied { get; protected set; }
        public ActivityHint Hint { get; set; }
        

        public virtual IDictionary<string, object> Properties
        {
            get { return new Dictionary<string, object>();}
        }

        protected ActivityBase(IGameLog log, Player player, string message, ActivityType type, ICard source)
        {
            Log = log;
            Player = player;
            Message = message;
            Type = type;
            Id = Guid.NewGuid();
            Hint = ActivityHint.None;

            Source = source == null ? string.Empty : source.Name;
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

    public enum ActivityHint
    {
        None,
        DiscardCards,
        PassCards,
        RedrawCards,
        GainCards,
        TrashCards,
        PlayCards
    }
}
