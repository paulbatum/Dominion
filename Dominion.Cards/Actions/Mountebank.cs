using System;
using System.Linq;
using Dominion.Cards.Curses;
using Dominion.Cards.Treasure;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Mountebank : Card, IActionCard, IAttackCard
    {
        public Mountebank() : base(5)
        {
        }

        public void Play(TurnContext context)
        {
            context.MoneyToSpend += 2;
            context.AddEffect(new MountebankAttack());
        }

        public class MountebankAttack : AttackEffect
        {
            public override void Attack(Player player, TurnContext context)
            {
                var curse = player.Hand.OfType<Curse>().FirstOrDefault();
                var cursePile = context.Game.Bank.Piles.SingleOrDefault(x => x.TopCard is Curse);
                var copperPile = context.Game.Bank.Piles.SingleOrDefault(x => x.TopCard is Copper);

                if(curse == null)
                {
                    context.Game.Log.LogMessage("{0} did not have a Curse to discard.", player.Name);
                    var copper = copperPile.TopCard;
                    copper.MoveTo(player.Discards);
                    context.Game.Log.LogGain(player, copper);

                    if (cursePile != null)
                    {
                        var card = cursePile.TopCard;
                        card.MoveTo(player.Discards);
                        context.Game.Log.LogGain(player, card);
                    }
                }
                else
                {
                    curse.MoveTo(player.Discards);
                    context.Game.Log.LogDiscard(player, curse);
                }
            }
        }
    }
}