using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    class MiningVillage : Card, IActionCard
    {
        public MiningVillage() : base(4)
        {
        }

        public void Play(TurnContext context)
        {
            context.DrawCards(1);
            context.RemainingActions += 2;
            context.AddEffect(this, new MiningVillageEffect(this));
        }

        private class MiningVillageEffect : CardEffectBase
        {
            private MiningVillage _source;

            public MiningVillageEffect(MiningVillage source)
            {
                _source = source;
            }

            public override void Resolve(TurnContext context, ICard source)
            {
                if (!context.Game.Trash.Contains(_source))
                {
                    var activity = Activities.ChooseYesOrNo(context.Game.Log, context.ActivePlayer,
                                                            "Trash mining village for +2 buy?",
                                                            source,
                                                            () =>
                                                            {

                                                                context.Trash(context.ActivePlayer, _source);
                                                                context.AvailableSpend += 2;

                                                            }
                        );

                    _activities.Add(activity);
                }
            }
        }
    }
}
