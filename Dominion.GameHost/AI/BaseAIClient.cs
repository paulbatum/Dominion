using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Dominion.GameHost.AI
{
    public abstract class BaseAIClient : GameClient
    {
        public BaseAIClient(Guid playerId, string playerName)
            : base (playerId, playerName)
        {
        }

        private long mLastGameStateVersionHandled = -1;

        public override void RaiseGameStateUpdated()
        {
            var state = GetGameState();
            _subject.OnNext(state);

            //This is to avoid double-handling events/activites
            if (mLastGameStateVersionHandled >= state.Version)
                return;
            mLastGameStateVersionHandled = state.Version;

            //Yep, this is really lame. 
            //Without this, AI actions don't show up for the human players until CometController.GameStateAsync() 
            //times out and sends them the latest.
            //It's guessed that this happens because the AI does all its actions before the human player 
            //has re-called GameStateAsync, and it doesn't report things that happen in-between long polls.
            Thread.Sleep(200);

            var activity = state.PendingActivity;
            if (activity != null)
                HandleActivity(activity, state);

            if (state.Status.IsActive)
                DoTurn(state);

            _subject.OnNext(GetGameState());
        }

        protected virtual void HandleActivity(ActivityModel activity, GameViewModel state)
        {
            switch (activity.Type)
            {
                case "SelectFixedNumberOfCards":
                {
                    int cardsToDiscard = int.Parse(activity.Properties["NumberOfCardsToSelect"].ToString());
                    DiscardCards(cardsToDiscard, state);
                    break;
                }
            }
        }

        protected abstract void DiscardCards(int count, GameViewModel currentState);

        protected abstract void DoTurn(GameViewModel currentState);
    }
}
