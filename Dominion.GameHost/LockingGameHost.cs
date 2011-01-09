using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Dominion.Cards.Actions;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.GameHost
{
    public interface IGameHost
    {
        void RegisterGameClient(IGameClient client, Player associatedPlayer);
        GameViewModel GetGameState(IGameClient client);
        IList<CardViewModel> GetDecklist(IGameClient client);
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

        public IList<CardViewModel> GetDecklist(IGameClient client)
        {
            _lock.EnterReadLock();
            try
            {
                var player = _players[client];

                var cards = new List<ICard>();
                cards.AddRange(player.Deck.Contents);
                cards.AddRange(player.Discards);
                cards.AddRange(player.Hand);
                cards.AddRange(player.PlayArea);

                return cards
                    .Select(c => new CardViewModel(c))
                    .OrderBy(c => c.Name)
                    .ToList();
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
            bool repeat = true;
            while (repeat)
            {
                repeat = false;

                foreach (var player in _game.Players)
                {
                    var activity = _game.GetPendingActivity(player);

                    if (activity is SelectReactionActivity)
                    {
                        var reactionActivity = ((SelectReactionActivity) activity);
                        var reactions = player.Hand.OfType<IReactionCard>();

                        // This could happen if a player uses a secret chamber and hides it, leaving no reactions in hand.
                        // An argument could be made for checking this in the SelectReactionActivity somehow. It seemed easier to do it here.
                        if (reactions.Count() == 0)
                        {
                            reactionActivity.CloseWindow();
                            repeat = true;
                        }

                        if (reactions.Select(r => r.Name).Distinct().Count() == 1)
                        {
                            // All the reaction cards are of the same type.
                            var reaction = reactions.First();

                            if (!reaction.ContinueReactingIfOnlyReaction)
                            {
                                // It doesn't make sense for the reaction window to stay open after playing this
                                // reaction, so its safe to play it and then close the window.
                                reactionActivity.SelectReaction(reaction);
                                reactionActivity.CloseWindow();
                                repeat = true;
                            }
                        }
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