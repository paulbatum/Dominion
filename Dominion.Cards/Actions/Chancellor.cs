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
                var choiceActivity = new ChoiceActivity(context, context.ActivePlayer,
                    "Do you wish to put your deck into your discard pile?",
                    Choice.Yes, Choice.No);
                choiceActivity.ActOnChoice = c => Execute(context, context.ActivePlayer, c);

                _activities.Add(choiceActivity);
            }

            private void Execute(TurnContext context, Player player, Choice choice)
            {
                if (choice == Choice.Yes)
                {
                    context.Game.Log.LogMessage("{0} put his deck in his discard pile", player);
                    player.Deck.MoveAll(player.Discards);
                }
            }
        }
    }
}