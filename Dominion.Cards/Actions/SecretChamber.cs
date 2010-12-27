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
            currentTurn.AddEffect(this, new SecretChamberReactionEffect(player));
            currentTurn.Game.Log.LogMessage("{0} revealed a Secret Chamber.", player);

        }

        public bool ContinueReactingIfOnlyReaction
        {
            get { return true; }
        }        

        public void Play(TurnContext context)
        {
            context.AddEffect(this, new SecretChamberActionEffect());
        }

        private class SecretChamberReactionEffect : CardEffectBase
        {
            private readonly Player _player;

            public SecretChamberReactionEffect(Player player)
            {
                _player = player;
            }

            public override void Resolve(TurnContext context, ICard source)
            {                
                _player.DrawCards(2);
                foreach (var activity in Activities.PutMultipleCardsFromHandOnTopOfDeck(context.Game.Log, _player, 2, source))
                    _activities.Add(activity);
            }
        }

        private class SecretChamberActionEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context, ICard source)
            {
                var activity = new SelectCardsActivity(
                    context,
                    "Select any number of cards to discard, you will gain $1 per card",
                    SelectionSpecifications.SelectUpToXCards(context.ActivePlayer.Hand.CardCount), source);

                activity.AfterCardsSelected = cards =>
                {
                    context.DiscardCards(activity.Player, cards);
                    context.AvailableSpend += cards.Count();                    
                };
                
                _activities.Add(activity);
            }
        }
    }

    public class Ambassador : Card, IActionCard, IAttackCard
    {
        public Ambassador() : base(3)
        {
        }

        public void Play(TurnContext context)
        {
            context.AddEffect(this, new AmbassadorEffect(this));
        }

        public class AmbassadorEffect : CardEffectBase
        {
            private readonly Ambassador _source;

            public AmbassadorEffect(Ambassador source)
            {
                _source = source;
            }

            public override void Resolve(TurnContext context, ICard source)
            {
                if (context.ActivePlayer.Hand.CardCount > 0)
                {
                    SelectCardsActivity revealActivity = GetRevealActivity(context, source);
                    _activities.Add(revealActivity);
                }
            }

            private SelectCardsActivity GetRevealActivity(TurnContext context, ICard source)
            {
                var revealActivity = new SelectCardsActivity(context, "Select a card to reveal.",
                                                             SelectionSpecifications.SelectExactlyXCards(1), _source);

                revealActivity.AfterCardsSelected = cards =>
                {
                    var selection = cards.Single();
                    var returnActivity = GetReturnActivity(context, selection);
                    _activities.Add(returnActivity);
                };

                return revealActivity;
            }

            private SelectCardsActivity GetReturnActivity(TurnContext context, ICard selection)
            {
                var returnActivity = new SelectCardsActivity(context,
                    string.Format("Select up to two {0} to return to the supply", selection.Name),
                    SelectionSpecifications.SelectUpToXCardsOfSameName(selection.Name, 2), 
                    _source);

                returnActivity.AfterCardsSelected = cards => ReturnCardsAndAttack(cards, context, selection.Name);
                return returnActivity;
            }


            private void ReturnCardsAndAttack(IEnumerable<ICard> selection, TurnContext context, string name)
            {
                var pile = context.Game.Bank.Piles.SingleOrDefault(p => p.Name == name);

                if (pile != null)
                {
                    foreach (var card in selection)
                        card.MoveTo(pile);

                    context.AddEffect(_source, new AmbassadorAttack(pile));
                }
            }
        }

        public class AmbassadorAttack : AttackEffect
        {
            private readonly CardPile _pile;

            public AmbassadorAttack(CardPile pile)
            {
                _pile = pile;
            }

            public override void Attack(Player victim, TurnContext context, ICard source)
            {
                var utility = new GainUtility(context, victim);
                utility.Gain(_pile);
            }
        }
    }
}