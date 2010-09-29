using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules.Activities
{
    public class SelectReactionActivity : SelectCardsActivity
    {
        private readonly TurnContext _currentTurn;
        private readonly AttackEffect _attackEffect;

        public SelectReactionActivity(TurnContext currentTurn, Player player, AttackEffect attackEffect)
            : base(currentTurn.Game.Log, player, "Select a reaction to use, click Done when finished.", SelectionSpecifications.SelectUpToXCards(1))
        {
            _currentTurn = currentTurn;
            _attackEffect = attackEffect;
            Specification.CardTypeRestriction = typeof (IReactionCard);
        }

        public void SelectReaction(IReactionCard reaction)
        {
            if (reaction == null)
                IsSatisfied = true;
            else
                reaction.React(_attackEffect, this.Player, _currentTurn);
        }

        public override void SelectCards(IEnumerable<Card> cards)
        {
            CheckCards(cards);

            var reaction = cards.OfType<IReactionCard>().SingleOrDefault();
            SelectReaction(reaction);  
        }


        public void CloseWindow()
        {
            SelectReaction(null);
        }
    }
}