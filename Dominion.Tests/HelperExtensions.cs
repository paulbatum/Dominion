using Dominion.Rules;

namespace Dominion.Tests
{
    public static class HelperExtensions
    {
        public static CardZone AddNewCards<T>(this CardZone targetZone, int count) where T : Card, new()
        {
            count.Times(() => new T().MoveTo(targetZone));
            return targetZone;
        }
    }
}