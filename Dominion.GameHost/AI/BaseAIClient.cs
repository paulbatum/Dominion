using System;
using System.Collections.Generic;
using System.Concurrency;
using System.Linq;
using System.Text;
using System.Threading;

namespace Dominion.GameHost.AI
{
    public abstract class BaseAIClient
    {
        private long _lastGameStateVersionHandled;
        private Guid _lastActivityHandled = Guid.Empty;
        protected IGameClient _client;
        private object _gate = new object();

        public void Attach(IGameClient client)
        {            
            _client = client;
            client.GameStateUpdates.ObserveOn(Scheduler.NewThread)
                .Delay(TimeSpan.FromMilliseconds(new Random(client.GetHashCode()).Next(500, 1500)))
                .Subscribe(Respond);
        }

        protected virtual void Respond(GameViewModel state)
        {
            lock (_gate)
            {
                //This is to avoid double-handling events
                if (_lastGameStateVersionHandled >= state.Version)
                    return;
                _lastGameStateVersionHandled = state.Version;

                var activity = state.PendingActivity;

                if (activity != null && _lastActivityHandled != activity.Id)
                {
                    _lastActivityHandled = activity.Id;
                    HandleActivity(activity, state);
                }
                else if (activity == null && state.Status.IsActive)
                {
                    var message = DoTurn(state);
                    _client.AcceptMessage(message);
                }
            }
        }

        protected IList<CardPileViewModel> GetValidBuys(GameViewModel state)
        {
            return (from pile in state.Bank
                    where (!pile.IsLimited) || pile.Count > 0
                    where state.Status.MoneyToSpend >= pile.Cost
                    select pile).ToList();
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

        protected abstract IGameActionMessage DoTurn(GameViewModel currentState);
    }
}
