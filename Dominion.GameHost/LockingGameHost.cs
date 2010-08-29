using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Dominion.Rules;

namespace Dominion.GameHost
{
    public class LockingGameHost
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
                message.UpdateGameState(_game);
            }
            finally
            {
                _lock.ExitWriteLock();
            }

            NotifyClients();
        }

        //public IGameClient ActiveClient
        //{
        //    get
        //    {
        //        _lock.EnterReadLock();
        //        try
        //        {
        //            return _players[]
        //        }
        //        catch (Exception)
        //        {
                    
        //            throw;
        //        }
        //    }
        //}

        private void NotifyClients()
        {
            foreach (var client in _players.Keys)
                client.GameStateUpdated();
        }
    }

    public interface IGameActionMessage
    {
        void UpdateGameState(Game game);
    }

    public class BuyCardMessage : IGameActionMessage
    {
        public Guid PileId { get; set; }        

        public void UpdateGameState(Game game)
        {
            var pile = game.Bank.Piles.Single(p => p.Id == PileId);
            game.CurrentTurn.Buy(pile);
        }
    }

    public interface IGameClient
    {
        void GameStateUpdated();
    }

    public class GameClient : IGameClient
    {
        public string PlayerName { get; set; }
        public void GameStateUpdated()
        {
            //TODO: Implement
        }
    }
}