using System.Linq;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules
{
    public class ReactionEffect : CardEffectBase
    {
        private readonly AttackEffect _attackEffect;

        public ReactionEffect(AttackEffect attackEffect)
        {
            _attackEffect = attackEffect;            
        }

        public override void Resolve(TurnContext context)
        {
            foreach(var opponent in context.Opponents)
            {
                var reactions = opponent.Hand.OfType<IReactionCard>();

                if(reactions.Count() > 0)
                    _activities.Add(new SelectReactionActivity(context, opponent, _attackEffect));                
            }
        }

       
    }
}