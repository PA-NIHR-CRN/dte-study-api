using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GovUk.AspNetCore.Mvc.Models;

public record GovUkRadiosModel (
    GovUkRadioSize Size,
    GovUkRadioLayout Layout,
    TagHelperContent InnerContent,
    string? Legend = null,
    string? LegendHeadingLevel = null,
    string? ErrorMessage = null) : GovUkModelWithContent(InnerContent);