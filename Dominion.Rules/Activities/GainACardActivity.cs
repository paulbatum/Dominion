using System;

namespace Dominion.Rules.Activities
{
    public class GainACardActivity : SelectPileActivity
    {
        private readonly CardZone _destination;

        public Action<ICard> AfterCardGained { get; set; }

        public GainACardActivity(IGameLog log, Player player, string message, ISelectionSpecification specification, CardZone destination, ICard source) 
            : base(log, player, message, specification, source)
        {
            _destination = destination;
            Hint = ActivityHint.GainCards;
        }

        public override void SelectPile(CardPile pile)
        {
            base.SelectPile(pile);

            var card = pile.TopCard;
            card.MoveTo(_destination);
            Log.LogGain(Player, card);

            if (AfterCardGained != null)
                AfterCardGained(card);
        }
    }
}