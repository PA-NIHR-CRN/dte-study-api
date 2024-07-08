﻿using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BPOR.Rms
{
    public static class ViewDataExtensions
    {
        public static void ShowBackLink(this ViewDataDictionary viewData, bool? showBackLink = true)
        {
            viewData["ShowBackLink"] = showBackLink;
        }

        public static bool? IsBackLinkEnabled(this ViewDataDictionary viewData)
        {
            return viewData["ShowBackLink"] as bool?;
        }
    }
}