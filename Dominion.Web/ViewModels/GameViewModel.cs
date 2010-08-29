using System;
using System.Linq;
using System.Web.Mvc;
using Dominion.GameHost;
using Dominion.Rules;

namespace Dominion.Web.ViewModels
{


    public static class CardImageHelper
    {
        public static string ResolveCardImage(this UrlHelper urlHelper, string cardName)
        {
            return urlHelper.Content(string.Format("~/Content/Images/Cards/{0}.jpg", cardName));
        }
    }
}