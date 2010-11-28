using System.Collections.Generic;
using System.Linq;

namespace Dominion.GameHost.AI
{
    public class MilitiaAI : OldBigMoneyAI
    {
        protected override IList<string> GetPriorities(GameViewModel state)
        {
            var priorites = base.GetPriorities(state);
            priorites.Insert(priorites.IndexOf("Silver"), "Militia");

            return priorites;
        }

        protected override IGameActionMessage DoTurn(GameViewModel state)
        {
            var militia = state.Hand.FirstOrDefault(c => c.Name == "Militia");
            
            if (state.Status.InBuyStep || militia == null)
                return base.DoTurn(state);
            else
                return new PlayCardMessage(_client.PlayerId, militia.Id);
        }
    }
}