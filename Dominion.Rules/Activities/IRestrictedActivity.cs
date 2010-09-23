using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Rules.Activities
{
    public interface IRestrictedActivity
    {
        IList<RestrictionType> Restrictions { get; }
    }
}
