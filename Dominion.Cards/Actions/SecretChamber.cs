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
                _activities.Add(new SelectCardToPutBackOnDeckActivity(_player, context, "Select the first card to put on top of the deck."));
                _activities.Add(new SelectCardToPutBackOnDeckActivity(_player, context, "Select the second card to put on top of the deck."));
            }
        }

        public void Play(TurnContext context)
        {
            context.AddEffect(new SecretChamberActionEffect(context));
        }

        public class SecretChamberActionEffect : CardEffectBase
        {
            public SecretChamberActionEffect(TurnContext context)
            {

            }

            public override void Resolve(TurnContext context)
            {
                _activities.Add(new SecretChamberDiscardActivity(context));
            }

            public class SecretChamberDiscardActivity : SelectAnyNumberOfCardsFromHandActivity
            {
                private readonly TurnContext _currentTurn;

                public SecretChamberDiscardActivity(TurnContext currentTurn)
                    : base(currentTurn.Game.Log, currentTurn.ActivePlayer, "Select any number of cards to discard")
                {
                    _currentTurn = currentTurn;
                }

                public override void Execute(IEnumerable<Card> cards)
                {
                    foreach (var card in cards.ToList())
                    {
                        Log.LogDiscard(Player, card);
                        card.MoveTo(Player.Discards);
                        _currentTurn.MoneyToSpend++;
                    }
                }
            }
        }
    }


}