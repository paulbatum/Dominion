using System;
using System.Collections.Generic;

namespace Dominion.GameHost
{
    public interface IGameClient
    {
        Guid PlayerId { get; }
        void RaiseGameStateUpdated();
        IObservable<GameViewModel> GameStateUpdates { get; }
        IObservable<string> ChatMessages { get; }
        GameViewModel GetGameState();
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
        private readonly Subject<GameViewModel> _subject = new Subject<GameViewModel>();        

        public void RaiseGameStateUpdated()
        {
            var state = GetGameState();
            _subject.OnNext(state);
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
            _host.SendChatMessage(message);
        }
    }

}