using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards
{
    public class GainUtility
    {
        private readonly IGameLog _log;
        private readonly Player _player;
        private readonly CardBank _bank;

        public GainUtility(TurnContext context, Player player)
            :this(context.Game.Log, context.Game.Bank, player)
        { }

        public GainUtility(IGameLog log, CardBank bank, Player player)
        {
            _log = log;
            _player = player;
            _bank = bank;
        }

        public void Gain<T>(Action<T> doMove) where T : Card
        {
            var pile = _bank.NonEmptyPile<T>();
            if (pile != null)
            {
                var card = (T) pile.TopCard;
                doMove(card);                
                _log.LogGain(_player, card);
            }
            else
            {
                _log.LogMessage("{0} did not gain a {1} because the pile is empty", _player.Name, typeof(T).Name);                
            }
        }

        public void Gain<T>() where T : Card
        {
            Gain<T>(card => card.MoveTo(_player.Discards));           
        }
    }

    public class PlayCardUtility
    {
        private readonly TurnContext _context;

        public PlayCardUtility(TurnContext context)
        {
            _context = context;
        }

        public void Play(IEnumerable<IActionCard> cards)
        {
            foreach(var card in cards.ToList())
            {
                _context.Game.Log.LogPlay(_context.ActivePlayer, card);
                card.Play(_context);
                card.MoveTo(_context.ActivePlayer.PlayArea);
            }
        }
    }
}