using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using Dominion.Rules.Activities;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public abstract class BehaviourBasedAI : BaseAIClient
    {
        protected IList<IAIBehaviour> Behaviours { get; set; }

        public BehaviourBasedAI()
        {
            Behaviours = new List<IAIBehaviour>();
        }

        protected override void HandleActivity(ActivityModel activity, GameViewModel state)
        {
            if (activity.ParseType() == ActivityType.WaitingForOtherPlayers)
                return;

            var behaviour = Behaviours.FirstOrDefault(b => b.CanRespond(activity, state));

            if(behaviour == null)
                throw new NoBehaviourException(activity);

            behaviour.Respond(_client, activity, state);
        }
    }

    public interface IAIBehaviour
    {
        bool CanRespond(ActivityModel activity, GameViewModel state);
        void Respond(IGameClient client, ActivityModel activity, GameViewModel state);
    }

    [Serializable]
    public class NoBehaviourException : Exception
    {
        public ActivityModel ActivityToHandle { get; set; }

        public NoBehaviourException()
        {
        }

        public NoBehaviourException(ActivityModel activity)
            : base("No behaviour exists to handle the given activity.")
        {
            ActivityToHandle = activity;
        }

        public NoBehaviourException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NoBehaviourException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}