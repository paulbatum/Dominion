using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Remodel : Card, IActionCard
    {
        public Remodel()
            : base(4)
        {
        }

        public void Play(TurnContext context)
        {
            context.AddEffect(new RemodelEffect(context));
        }

        public class RemodelEffect : CardEffectBase
        {
            public RemodelEffect(TurnContext context)
            {
                _activities.Add(new SelectCardToRemodelActivity(context, this));
            }

            public void AddGainActivity(IGameLog log, Player player, int upToCost)
            {
                _activities.Add(new GainACardUpToActivity(log, player, upToCost));
            }

            public class SelectCardToRemodelActivity : SelectCardsFromHandActivity
            {
                private readonly TurnContext _context;
                private readonly RemodelEffect _remodelEffect;

                public SelectCardToRemodelActivity(TurnContext context, RemodelEffect remodelEffect) 
                    : base(context.Game.Log, context.ActivePlayer, "Select a card to remodel", ActivityType.SelectFixedNumberOfCards, 1)
                {
                    _context = context;
                    _remodelEffect = remodelEffect;
                }

                public override void Execute(IEnumerable<Card> cards)
                {
                    var cardToRemodel = cards.Single();

                    _context.Trash(this.Player, cardToRemodel);
                    _remodelEffect.AddGainActivity(this.Log, this.Player, cardToRemodel.Cost + 2);
                }
            }
        }
    }
}