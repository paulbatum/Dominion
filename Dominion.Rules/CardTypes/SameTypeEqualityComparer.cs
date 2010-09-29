using System;
using System.Collections.Generic;

namespace Dominion.Rules.CardTypes
{
    public class SameTypeEqualityComparer<T> : EqualityComparer<T>
    {
        public override bool Equals(T x, T y)
        {
            return GetHashCode(x) == GetHashCode(y);
        }

        public override int GetHashCode(T obj)
        {
            return obj.GetType().GetHashCode();
        }
    }
}