using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.GameHost
{
    public interface IGameHost
    {
        void RegisterGameClient(IGameClient client, Player associatedPlayer);
        GameViewModel GetGameState(IGameClient client);
        void AcceptMessage(IGameActionMessage message);
    }

    public class LockingGameHost : IGameHost
    {
        private readonly Game _game;
        private readonly IDictionary<IGameClient, Player> _players;
        private readonly ReaderWriterLockSlim _lock;

        public LockingGameHost(Game game)
        {
            _game = game;
            _players = new Dictionary<IGameClient, Player>();      
            _lock = new ReaderWriterLockSlim();
        }

        public void RegisterGameClient(IGameClient client, Player associatedPlayer)
        {
            _players[client] = associatedPlayer;
        }

        public GameViewModel GetGameState(IGameClient client)
        {
            _lock.EnterReadLock();
            try
            {
                return new GameViewModel(_game, _players[client]);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void AcceptMessage(IGameActionMessage message)
        {
            _lock.EnterWriteLock();
            try
            {
                message.Validate(_game);
                message.UpdateGameState(_game);

                AutomaticallyProgress();
            }
            finally
            {
                _lock.ExitWriteLock();
            }

            NotifyClients();
        }

        private void AutomaticallyProgress()
        {
            while(true)
            {
                if (_game.IsComplete)
                    break;

                TurnContext currentTurn = _game.CurrentTurn;
                if (currentTurn.InBuyStep)
                {
                    if (currentTurn.Buys == 0)
                    {
                        currentTurn.EndTurn();
                        continue;
                    }
                }
                else
                {
                    if(currentTurn.ActivePlayer.Hand.OfType<ActionCard>().Any() == false || currentTurn.RemainingActions == 0)
                    {
                        currentTurn.MoveToBuyStep();
                        continue;
                    }
                }

                break;
            }
        }

        private void NotifyClients()
        {
            foreach (var client in _players.Keys)
                client.RaiseGameStateUpdated(this);
        }
    }

    public interface IGameActionMessage
    {
        void UpdateGameState(Game game);
        void Validate(Game game);
    }

    public class BuyCardMessage : IGameActionMessage
    {
        public BuyCardMessage(Guid playerId, Guid pileId)
        {
            PlayerId = playerId;
            PileId = pileId;
        }

        public Guid PlayerId { get; private set; }
        public Guid PileId { get; private set; }        

        public void UpdateGameState(Game game)
        {            
            CardPile pile = game.Bank.Piles.Single(p => p.Id == PileId);
            game.CurrentTurn.Buy(pile);            
        }

        public void Validate(Game game)
        {
            if(game.ActivePlayer.Id != PlayerId)
                throw new InvalidOperationException(string.Format("Player '{0}' is not active.", PlayerId));
        }
    }

    public class PlayCardMessage : IGameActionMessage
    {
        public PlayCardMessage(Guid playerId, Guid cardId)
        {
            PlayerId = playerId;
            CardId = cardId;
        }

        public Guid PlayerId { get; private set; }
        public Guid CardId { get; private set; }

        public void UpdateGameState(Game game)
        {
            Card card = game.CurrentTurn.ActivePlayer.Hand.Single(c => c.Id == CardId);
            game.CurrentTurn.Play((ActionCard) card);            
        }

        public void Validate(Game game)
        {
            if (game.ActivePlayer.Id != PlayerId)
                throw new InvalidOperationException(string.Format("Player '{0}' is not active.", PlayerId));
        }
    }

    public class MoveToBuyStepMessage : IGameActionMessage
    {
        public MoveToBuyStepMessage(Guid playerId)
        {
            PlayerId = playerId;
        }

        public Guid PlayerId { get; private set; }

        public void UpdateGameState(Game game)
        {
            game.CurrentTurn.MoveToBuyStep();            
        }

        public void Validate(Game game)
        {
            if (game.ActivePlayer.Id != PlayerId)
                throw new InvalidOperationException(string.Format("Player '{0}' is not active.", PlayerId));
        }
    }

    public class EndTurnMessage : IGameActionMessage
    {
        public EndTurnMessage(Guid playerId)
        {
            PlayerId = playerId;
        }

        public Guid PlayerId { get; private set; }

        public void UpdateGameState(Game game)
        {
            game.CurrentTurn.EndTurn();            
        }

        public void Validate(Game game)
        {
            if (game.ActivePlayer.Id != PlayerId)
                throw new InvalidOperationException(string.Format("Player '{0}' is not active.", PlayerId));
        }
    }

    public class BeginGameMessage : IGameActionMessage
    {
        public void UpdateGameState(Game game)
        {
            TurnContext tempQualifier = game.CurrentTurn;
            if (tempQualifier.ActivePlayer.Hand.OfType<ActionCard>().Any() == false || tempQualifier.RemainingActions == 0)
                tempQualifier.MoveToBuyStep();
        }

        public void Validate(Game game)
        {
            
        }
    }

    public interface IGameClient
    {
        Guid PlayerId { get; }
        void RaiseGameStateUpdated(LockingGameHost host);
        IObservable<GameViewModel> GameStateUpdates { get; }
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

        private readonly Subject<GameViewModel> _subject = new Subject<GameViewModel>();

        public void RaiseGameStateUpdated(LockingGameHost host)
        {
            _subject.OnNext(host.GetGameState(this));
        }

        public IObservable<GameViewModel> GameStateUpdates
        {
            get { return _subject; }
        }
    }
}