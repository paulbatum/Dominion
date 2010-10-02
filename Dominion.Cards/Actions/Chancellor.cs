using System;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Chancellor : Card, IActionCard
    {
        public Chancellor() : base(3)
        {
        }

        public void Play(TurnContext context)
        {
            context.AvailableSpend += 2;
            context.AddEffect(new ChancellorEffect());
        }

        public class ChancellorEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context)
            {
                var player = context.ActivePlayer;
                var choiceActivity = Activities.ChooseYesOrNo(context.Game.Log, player,
                    "Do you wish to put your deck into your discard pile?",
                    () =>
                    {
                        context.Game.Log.LogMessage("{0} put his deck in his discard pile", player);
                        player.Deck.MoveAll(player.Discards);
                    });

                _activities.Add(choiceActivity);
            }
        }
    }
}