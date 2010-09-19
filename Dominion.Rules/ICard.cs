using System;
namespace Dominion.Rules
{
    public interface ICard
    {
        int Cost { get; }
        CardZone CurrentZone { get; }
        Guid Id { get; }
        void MoveTo(CardZone targetZone);
        string Name { get; }
        string ToString();
    }
}
