using System;
using System.Collections.Generic;

namespace Dominion.Rules.Activities
{
    public interface IActivity
    {
        string Message { get; }
        bool IsSatisfied { get; }
        Player Player { get; }
        ActivityType Type { get; }
        Guid Id { get; }
        IDictionary<string, object> Properties { get; }
        string Source { get; }
        ActivityHint Hint { get; }
    }
}