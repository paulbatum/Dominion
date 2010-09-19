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
            context.MoneyToSpend += 2;
            context.AddEffect(new ChancellorEffect());
        }

        public class ChancellorEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context)
            {
                _activities.Add(new ChancellorActivity(context.Game.Log, context.ActivePlayer, "Do you wish to put your deck into your discard pile?"));
            }

            public class ChancellorActivity : YesNoChoiceActivity
            {
                public ChancellorActivity(IGameLog log, Player player, string message)
                    : base(log, player, message)
                {
                }

                public override void Execute(bool choice)
                {
                    if (choice)
                    {
                        Log.LogMessage("{0} put his deck in his discard pile", Player.Name);
                        this.Player.Deck.MoveAll(this.Player.Discards);
                    }
                }
            }
        }

    }
}