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
            client.AssociateWithHost(this);
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
                _game.IncrementVersion();

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

                if (currentTurn.CurrentEffect != null)
                    break;

                if (currentTurn.InBuyStep)
                {
                    if (currentTurn.Buys == 0)
                    {
                        _game.EndTurn();
                        continue;
                    }
                }
                else
                {
                    if(currentTurn.ActivePlayer.Hand.OfType<IActionCard>().Any() == false || currentTurn.RemainingActions == 0)
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
                client.RaiseGameStateUpdated();
        }
    }

}