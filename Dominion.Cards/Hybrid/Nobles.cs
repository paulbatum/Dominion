using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Hybrid
{
    public class Nobles : Card, IVictoryCard, IActionCard
    {
        public Nobles() : base(6)
        {
        }

        public int Score(CardZone allCards)
        {
            return 2;
        }

        public void Play(TurnContext context)
        {
            context.AddEffect(new NoblesEffect());
        }

        public class NoblesEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context)
            {
                var choiceActivity = new ChoiceActivity(context, context.ActivePlayer, 
                    "Choose from: Draw 3 cards, gain 2 actions", 
                    Choice.DrawCards, Choice.GainActions);
                choiceActivity.ActOnChoice = c => Execute(context, c);

                _activities.Add(choiceActivity);
            }

            private void Execute(TurnContext context, Choice choice)
            {
                switch (choice)
                {
                    case Choice.DrawCards:
                        context.ActivePlayer.DrawCards(3);
                        return;
                    case Choice.GainActions:
                        context.RemainingActions += 2;
                        return;
                }

                string error = string.Format("Choice of '{0}' invalid. ChoiceActivity shouldn't have allowed this to be called", choice);
                throw new Exception(error);
            }
        }
    }
}
