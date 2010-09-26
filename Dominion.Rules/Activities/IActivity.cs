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
        void WriteProperties(IDictionary<string, object> bag);
    }
}