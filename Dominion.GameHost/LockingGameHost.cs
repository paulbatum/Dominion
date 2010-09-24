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
        void SendChatMessage(string message);
        IObservable<string> ChatMessages { get; }
    }

    public class LockingGameHost : IGameHost
    {
        private readonly Game _game;
        private readonly IDictionary<IGameClient, Player> _players;
        private readonly ReaderWriterLockSlim _lock;
        private readonly Subject<string> _chatSubject;

        public LockingGameHost(Game game)
        {
            _game = game;
            _players = new Dictionary<IGameClient, Player>();      
            _lock = new ReaderWriterLockSlim();
            _chatSubject = new Subject<string>();        
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

                AutomaticallyReact();
                AutomaticallyProgress();
                
            }
            finally
            {
                _lock.ExitWriteLock();
            }

            NotifyClients();
        }

        public IObservable<string> ChatMessages
        {
            get { return _chatSubject; }
        }

        public void SendChatMessage(string message)
        {
            _chatSubject.OnNext(message);
        }


        private void AutomaticallyReact()
        {
            foreach(var player in _game.Players)
            {
                var activity = _game.GetPendingActivity(player);
                
                if(activity is SelectReactionActivity)                    
                {
                    var reactions = player.Hand.OfType<IReactionCard>();

                    if(reactions.Select(c => c.Name).Distinct().Count() == 1)
                    {
                        ((SelectReactionActivity) activity).SelectCards(reactions.Cast<Card>().Take(1));
                    }
                    
                }
            }
        }

        private void AutomaticallyProgress()
        {
            while(true)
            {
                if (_game.IsComplete)
                    break;                

                TurnContext currentTurn = _game.CurrentTurn;

                if (currentTurn.GetCurrentEffect() != null)
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