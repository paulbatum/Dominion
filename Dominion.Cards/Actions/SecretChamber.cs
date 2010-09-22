using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class SecretChamber : Card, IActionCard, IReactionCard
    {
        public SecretChamber()
            : base(2)
        {
        }

        public void React(AttackEffect attackEffect, Player player, TurnContext currentTurn)
        {
            currentTurn.AddEffect(new SecretChamberReactionEffect(player));
        }

        public class SecretChamberReactionEffect : CardEffectBase
        {
            private readonly Player _player;

            public SecretChamberReactionEffect(Player player)
            {
                _player = player;
            }

            public override void Resolve(TurnContext context)
            {
                _player.DrawCards(2);
                _activities.Add(new SelectCardsToPutBackOnDeckActivity(_player, context, 2));
            }
        }

        public void Play(TurnContext context)
        {
            //context.AddEffect(new SecretChamberActionEffect(context));
        }

        //public class SecretChamberActionEffect : CardEffectBase
        //{
        //    public SecretChamberActionEffect(TurnContext context)
        //    {

        //    }

        //    public override void Resolve(TurnContext context)
        //    {
        //        _activities.Add(new SecretChamberDiscardActivity(context));
        //    }

        //    public class SecretChamberDiscardActivity : SelectCardsFromHandActivity
        //    {
        //        public SecretChamberDiscardActivity(IGameLog log, Player player, int numberToDiscard)
        //            : base(log, player, string.Format("Select any number of cards to discard", numberToDiscard), ActivityType.SelectFixedNumberOfCards, numberToDiscard)
        //        { }

        //        public override void Execute(IEnumerable<Card> cards)
        //        {
        //            foreach (var card in cards.ToList())
        //            {
        //                Log.LogDiscard(Player, card);
        //                card.MoveTo(Player.Discards);
        //            }
        //        }
        //    }
        //}
    }


}