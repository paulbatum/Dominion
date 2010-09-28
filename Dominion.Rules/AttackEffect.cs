using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules
{
    public abstract class AttackEffect : CardEffectBase
    {
        private bool _hasHadReactionStep;
        private IList<Player> _nullifications = new List<Player>();

        public override void BeginResolve(TurnContext context)
        {
            if(_hasHadReactionStep)
            {
                base.BeginResolve(context);
            }
            else
            {
                _hasHadReactionStep = true;
                if (context.Opponents.Any(o => o.Hand.OfType<IReactionCard>().Any()))
                    context.AddEffect(new ReactionEffect(this));
                else
                    base.BeginResolve(context);
            }
        }

        public override void Resolve(TurnContext context)
        {
            foreach(var opponent in context.Opponents.Where(o => !_nullifications.Contains(o)))
                Attack(opponent, context);
        }

        public abstract void Attack(Player player, TurnContext context);

        public void Nullify(Player player)
        {            
            _nullifications.Add(player);
        }
    }

}