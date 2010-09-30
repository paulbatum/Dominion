using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Expand : Card, IActionCard
    {
        public Expand() : base(7)
        {
        }

        public void Play(TurnContext context)
        {
            context.AddEffect(new ExpandEffect());
        }
        
        public class ExpandEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context)
            {
                var remodelActivity = new SelectCardsActivity(context, "Select a card to expand",
                    SelectionSpecifications.SelectExactlyXCards(1));

                remodelActivity.AfterCardsSelected = cardList =>
                {
                    var player = context.ActivePlayer;
                    var cardToRemodel = cardList.Single();
                    context.Trash(player, cardToRemodel);

                    var gainActivity = Activities.GainACardCostingUpToX(context.Game.Log, player, cardToRemodel.Cost + 3);
                    _activities.Add(gainActivity);
                };

                _activities.Add(remodelActivity);
            }
        }
    }
}