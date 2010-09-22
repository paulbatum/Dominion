using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules
{
    public class SelectReactionActivity : SelectCardsFromHandActivity
    {
        private readonly TurnContext _currentTurn;
        private readonly AttackEffect _attackEffect;

        public SelectReactionActivity(TurnContext currentTurn, Player player, AttackEffect attackEffect)
            : base(currentTurn.Game.Log, player, "Select a reaction to play, pass when done.", ActivityType.SelectFixedNumberOfCards, 1)
        {
            _currentTurn = currentTurn;
            _attackEffect = attackEffect;
            this.Restrictions.Add(RestrictionType.ReactionCard);
        }

        public override void Execute(IEnumerable<Card> cards)
        {
            var reaction = cards.OfType<IReactionCard>().Single();
            reaction.React(_attackEffect, this.Player, _currentTurn);
        }
    }
}