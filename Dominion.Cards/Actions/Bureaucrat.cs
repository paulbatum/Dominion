using System.Linq;
using Dominion.Cards.Treasure;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Bureaucrat : Card, IActionCard, IAttackCard
    {
        public Bureaucrat() : base(4)
        {}

        public void Play(TurnContext context)
        {
            var gainUtil = new GainUtility(context, context.ActivePlayer);
            gainUtil.Gain<Silver>(card => context.ActivePlayer.Deck.MoveToTop(card));
            context.AddEffect(this, new BureaucratAttack());
        }

        public class BureaucratAttack : AttackEffect
        {
            public override void Attack(Player victim, TurnContext context, ICard source)
            {
                var victoryTypes = victim.Hand.OfType<IVictoryCard>()
                    .WithDistinctTypes()
                    .ToList();

                if(victoryTypes.Count() > 1)
                {
                    var activity = Activities.PutCardOfTypeFromHandOnTopOfDeck(context.Game.Log, victim,
                                                                               "Select a victory card to put on top",
                                                                               typeof (IVictoryCard),
                                                                               source);
                   _activities.Add(activity);
                }
                else if(victoryTypes.Any())
                {
                    var card = victoryTypes.Single();
                    victim.Deck.MoveToTop(card);
                    context.Game.Log.LogMessage("{0} put a {1} on top of the deck.", victim.Name, card.Name);
                }
                else
                {
                    context.Game.Log.LogRevealHand(victim);
                }
            }
        }
    }
}