using System;
using System.Linq;
using System.Text;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules
{
    public interface IGameLog
    {
        string Contents { get; }

        void LogTurn(Player player);
        void LogPlay(Player player, ActionCard card);
        void LogBuy(Player player, CardPile pile);
        void LogGameEnd(Game game);
        void LogDiscard(Player player, Card card);
        void LogMessage(string message, params object[] values);
        void LogGain(Player player, Card card);
    }

    public class TextGameLog : IGameLog
    {
        private readonly StringBuilder _builder = new StringBuilder();

        public string Contents
        {
            get { return _builder.ToString(); }
        }

        public void LogTurn(Player player)
        {
            _builder.AppendFormat("{0}'s turn has begun.", player.Name);
            _builder.AppendLine();
        }

        public void LogPlay(Player player, ActionCard card)
        {
            _builder.AppendFormat("{0} played a {1}.", player.Name, card.Name);
            _builder.AppendLine();
        }

        public void LogBuy(Player player, CardPile pile)
        {
            _builder.AppendFormat("{0} bought a {1}.", player.Name, pile.Name);
            _builder.AppendLine();
        }

        public void LogGameEnd(Game game)
        {
            var scores = game.Score();

            _builder
                .AppendLine("The game has ended!")
                .AppendLine()
                .AppendLine("----- SCORES -----");                

            foreach (var score in scores)
                _builder
                    .AppendFormat("{0}: {1}", score.PlayerName, score.Total)
                    .AppendLine();

            _builder.AppendLine("------------------");
            var winner = scores.Last(score => score.Total == scores.Max(x => x.Total));

            _builder.AppendLine();
            _builder.AppendLine(winner.PlayerName + " is the winner!");


        }

        public void LogDiscard(Player player, Card card)
        {
            _builder.AppendFormat("{0} discarded a {1}.", player.Name, card.Name);
            _builder.AppendLine();
        }

        public void LogMessage(string message, params object[] values)
        {
            _builder.AppendFormat(message, values);
            _builder.AppendLine();

        }

        public void LogGain(Player player, Card card)
        {
            _builder.AppendFormat("{0} gained a {1}", player.Name, card.Name);
            _builder.AppendLine();

        }
    }
}