using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Swindler : Card, IActionCard, IAttackCard
    {
        public Swindler() : base(3)
        {}

        public void Play(TurnContext context)
        {
            context.AvailableSpend += 2;
            context.AddEffect(this, new SwindlerAttack());
        }

        public class SwindlerAttack : AttackEffect
        {
            public override void Attack(Player victim, TurnContext context, ICard source)
            {
                var swindledCard = victim.Deck.TopCard;
                if(swindledCard == null)
                {
                    context.Game.Log.LogMessage("{0} did not have any cards to be swindled.", victim.Name);
                    return;
                }
                
                context.Trash(victim, swindledCard);
                var candidates = context.Game.Bank.Piles.Where(p => p.IsEmpty == false && p.TopCard.Cost == swindledCard.Cost);                

                if(candidates.Count() == 0)
                {
                    context.Game.Log.LogMessage("There are no cards of cost {0}.", swindledCard.Cost);
                }
                else if (candidates.Count() == 1)
                {
                    var pile = candidates.Single();
                    var card = pile.TopCard;
                    card.MoveTo(victim.Discards);
                    context.Game.Log.LogGain(victim, card);
                }
                else
                {
                    var activity = Activities.SelectACardForOpponentToGain(context, context.ActivePlayer, victim, swindledCard.Cost, source);
                    _activities.Add(activity);    
                }
            }
        }
    }
}