using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class ThroneRoom : ActionCard
    {
        public ThroneRoom() : base(4)
        {
        }

        protected override void Play(TurnContext context)
        {
            context.AddEffect(new ThroneRoomEffect(context));
        }

        public class ThroneRoomEffect : CardEffectBase
        {
            public ThroneRoomEffect(TurnContext context)
            {
                if(context.ActivePlayer.Hand.OfType<ActionCard>().Any())
                    _activities.Add(new ThroneRoomActivity(context));
            }

            public class ThroneRoomActivity : SelectCardsFromHandActivity
            {
                private readonly TurnContext _context;

                public ThroneRoomActivity(TurnContext context)
                    : base(context.Game.Log, context.ActivePlayer, "Select an action to play twice", ActivityType.SelectFixedNumberOfCards, 1)
                {
                    _context = context;
                    Restrictions.Add(RestrictionType.ActionCard);
                }

                public override void Execute(IEnumerable<Card> cards)
                {
                    var actionCard = cards.OfType<ActionCard>().Single();
                    _context.Game.Log.LogMessage("{0} selected {1} to be played twice.", Player.Name, actionCard.Name);

                    if (actionCard != null)
                    {
                        actionCard.MoveTo(this.Player.PlayArea);

                        actionCard.PlayFromOtherAction(_context);
                        actionCard.PlayFromOtherAction(_context);
                    }
                }
            }
        }
    }
}