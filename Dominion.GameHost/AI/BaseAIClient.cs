using System;
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
        private IDisposable _subscription;

        public void Attach(IGameClient client)
        {
            if (_client != null)
                _subscription.Dispose();

            _lastGameStateVersionHandled = 0;
            _lastActivityHandled = Guid.Empty;

            _client = client;
            _subscription = client.GameStateUpdates.ObserveOn(Scheduler.NewThread)
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

                if (_lastActivityHandled != activity.Id)
                {
                    _lastActivityHandled = activity.Id;                    
                    HandleActivity(activity, state);
                }
            }
        }

        protected abstract void HandleActivity(ActivityModel activity, GameViewModel state);

       
    }
}
