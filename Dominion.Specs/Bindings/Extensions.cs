using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules.Activities;

namespace Dominion.Specs.Bindings
{
    public static class Extensions
    {
        public static int GetCountProperty(this IActivity activity)
        {
            return (int) activity.Properties["NumberOfCardsToSelect"];
        }

        public static int GetCostProperty(this IActivity activity)
        {
            return (int) activity.Properties["Cost"];
        }

        public static string GetTypeRestrictionProperty(this IActivity activity)
        {
            return (string)activity.Properties["CardsMustBeOfType"];
        }
    }
}
