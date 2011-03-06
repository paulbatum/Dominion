using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Upgrade : Card, IActionCard
    {
        public Upgrade()
            : base(5)
        {
        }

        public void Play(TurnContext context)
        {
            context.DrawCards(1);
            context.RemainingActions += 1;

            context.AddEffect(this, new UpgradeEffect());
        }

        public class UpgradeEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context, ICard source)
            {
                if(context.ActivePlayer.Hand.CardCount == 0)
                {
                    context.Game.Log.LogMessage("{0} did not have any cards to upgrade.", context.ActivePlayer.Name);
                    return;
                }

                var upgradeActivity = new SelectCardsActivity(context, "Select a card to Upgrade",
                    SelectionSpecifications.SelectExactlyXCards(1), source);

                upgradeActivity.AfterCardsSelected = cardList =>
                {
                    var player = context.ActivePlayer;
                    var cardToUpgrade = cardList.Single();
                    var upgradeCost = cardToUpgrade.Cost + 1;
                    context.Trash(player, cardToUpgrade);

                    if (context.Game.Bank.Piles.Any(p => !p.IsEmpty && p.TopCard.Cost == upgradeCost))
                    {
                        var gainActivity = Activities.GainACardCostingExactlyX(context.Game.Log, player,
                            upgradeCost, player.Discards, source);
                        _activities.Add(gainActivity);
                    }
                    else
                    {
                        context.Game.Log.LogMessage("{0} could gain no card of appropriate cost", player);
                    }
                };

                _activities.Add(upgradeActivity);
            }
        }
    }
}
