using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Masquerade : Card, IActionCard
    {
        public Masquerade() : base(3)
        {}

        public void Play(TurnContext context)
        {
            context.DrawCards(2);
            context.AddEffect(new MasqueradeEffect());
        }

        public class MasqueradeEffect : CardEffectBase
        {
            private readonly Dictionary<ICard, CardZone> _cardMovements;
            private int _expectedMovementCount = 0;

            public MasqueradeEffect()
            {
                _cardMovements = new Dictionary<ICard, CardZone>();
            }            

            public override void Resolve(TurnContext context)
            {
                var passingPlayers = context.Game.Players.Where(p => p.Hand.CardCount > 0);
                _expectedMovementCount = passingPlayers.Count();

                foreach(var player in passingPlayers)
                {
                    Player tempPlayer = player;

                    var activity = (ISelectCardsActivity) new SelectCardsActivity
                        (context.Game.Log, player, "Select a card to pass.", SelectionSpecifications.SelectExactlyXCards(1))
                    {
                        AfterCardsSelected = cards =>
                        {
                            _cardMovements[cards.Single()] = context.Game.PlayerToLeftOf(tempPlayer).Hand;
                            if (_cardMovements.Keys.Count == _expectedMovementCount)
                            {
                                DoMovements();
                                DoTrash(context);
                            }
                        }
                    };

                    _activities.Add(activity);
                }
            }

            private void DoMovements()
            {
                foreach (var cardMovement in _cardMovements)
                    cardMovement.Key.MoveTo(cardMovement.Value);
            }

            private void DoTrash(TurnContext context)
            {
                if (context.ActivePlayer.Hand.CardCount > 0)
                {
                    var activity = Activities.SelectUpToXCardsToTrash(context, context.ActivePlayer, 1);
                    _activities.Add(activity);
                }
            }

        }
    }
}