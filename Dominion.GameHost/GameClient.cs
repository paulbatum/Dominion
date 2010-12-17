using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.GameHost
{
    public interface IGameClient
    {
        Guid PlayerId { get; }
        string PlayerName { get; }
        void RaiseGameStateUpdated();
        IObservable<GameViewModel> GameStateUpdates { get; }
        IObservable<string> ChatMessages { get; }
        GameViewModel GetGameState();
        IList<CardViewModel> GetDecklist();
        void AssociateWithHost(IGameHost gameHost);
        void AcceptMessage(IGameActionMessage message);
        void SendChatMessage(string message);
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
        protected readonly Subject<GameViewModel> _subject = new Subject<GameViewModel>();        

        public virtual void RaiseGameStateUpdated()
        {
            var state = GetGameState();
            _subject.OnNext(state);

            if(state.Status.GameIsComplete)
                _subject.OnCompleted();
        }

        public IObservable<GameViewModel> GameStateUpdates
        {
            get { return _subject; }
        }

        public IObservable<string> ChatMessages
        {
            get { return _host.ChatMessages; }
        }

        public GameViewModel GetGameState()
        {
            return _host.GetGameState(this);
        }

        public IList<CardViewModel> GetDecklist()
        {
            return _host.GetDecklist(this);
        }

        public void AssociateWithHost(IGameHost gameHost)
        {
            if(_host != null)
                throw new InvalidOperationException("Already associated with a host.");

            _host = gameHost;
        }

        public void AcceptMessage(IGameActionMessage message)
        {
            _host.AcceptMessage(message);
        }

        public void SendChatMessage(string message)
        {
            if(!string.IsNullOrEmpty(message))
                _host.SendChatMessage(string.Format("{0}: {1}", this.PlayerName,  message));
        }
    }

}