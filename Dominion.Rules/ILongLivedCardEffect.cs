using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules
{
    public interface ILongLivedCardEffect
    {
        ICard SourceCard { get; }
        void OnTurnStarting(TurnContext context);
        bool IsFinished { get; }
    }
    

}
