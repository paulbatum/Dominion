using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Rules
{
    public interface ILongLivedCardEffect
    {
        ICard SourceCard { get; }
        void OnTurnStarting(TurnContext context);
        bool IsFinished { get; }
    }
}
