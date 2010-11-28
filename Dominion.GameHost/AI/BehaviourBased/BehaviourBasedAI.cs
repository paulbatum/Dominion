using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

            var behaviour = Behaviours.First(b => b.CanRespond(activity, state));
            behaviour.Respond(_client, activity, state);
        }
    }

    public interface IAIBehaviour
    {
        bool CanRespond(ActivityModel activity, GameViewModel state);
        void Respond(IGameClient client, ActivityModel activity, GameViewModel state);
    }
}