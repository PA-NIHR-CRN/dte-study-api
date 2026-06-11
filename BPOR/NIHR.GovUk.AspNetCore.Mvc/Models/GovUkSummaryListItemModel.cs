using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GovUk.AspNetCore.Mvc.Models;

public record GovUkSummaryListItemModel (
    string Name,
    object Value,
    TagHelperContent InnerContent) : GovUkModelWithContent(InnerContent);