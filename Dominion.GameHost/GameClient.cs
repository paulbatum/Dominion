using System;
using System.Collections.Generic;

namespace Dominion.GameHost
{
    public interface IGameClient
    {
        Guid PlayerId { get; }
        void RaiseGameStateUpdated(IGameHost host);
        IObservable<GameViewModel> GameStateUpdates { get; }
        GameViewModel GetGameState();
        void AssociateWithHost(IGameHost gameHost);
    }

    public class GameClient : IGameClient
    {
        public GameClient(Guid playerId, string playerName)
        {
            PlayerId = playerId;
            PlayerName = playerName;
        }

        public Guid PlayerId { get; private set; }
        public string PlayerName { get; private set; }

        private IGameHost _host;
        private readonly Subject<GameViewModel> _subject = new Subject<GameViewModel>();

        public void RaiseGameStateUpdated(IGameHost  host)
        {
            _subject.OnNext(host.GetGameState(this));
        }

        public IObservable<GameViewModel> GameStateUpdates
        {
            get { return _subject; }
        }

        public GameViewModel GetGameState()
        {
            return _host.GetGameState(this);
        }

        public void AssociateWithHost(IGameHost gameHost)
        {
            if(_host != null)
                throw new InvalidOperationException("Already associated with a host.");

            _host = gameHost;
        }
    }
}