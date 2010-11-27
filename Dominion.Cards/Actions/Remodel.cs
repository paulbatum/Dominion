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
            context.AddEffect(this, new RemodelEffect());
        }

        public class RemodelEffect : CardEffectBase
        {
            private int _costIncrease;
            private string _message;

            public RemodelEffect()
                :this(2, "Select a card to Remodel.")
            {
                
            }

            protected RemodelEffect(int costIncrease, string message)
            {
                _costIncrease = costIncrease;
                _message = message;
            }

            public override void Resolve(TurnContext context, ICard source)
            {
                var remodelActivity = new SelectCardsActivity(context, _message, 
                    SelectionSpecifications.SelectExactlyXCards(1), source);

                remodelActivity.AfterCardsSelected = cardList =>
                {
                    var player = context.ActivePlayer;
                    var cardToRemodel = cardList.Single();
                    context.Trash(player, cardToRemodel);

                    var gainActivity = Activities.GainACardCostingUpToX(context.Game.Log, player, cardToRemodel.Cost + _costIncrease, source);
                    _activities.Add(gainActivity);
                };

                _activities.Add(remodelActivity);
            }
        }
    }
}