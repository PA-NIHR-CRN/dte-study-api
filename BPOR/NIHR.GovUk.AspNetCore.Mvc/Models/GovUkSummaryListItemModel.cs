using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GovUk.AspNetCore.Mvc.Models;

public record GovUkSummaryListItemModel(
    bool ShowName,
    string Name,
    object? Value,
    TagHelperContent? ValueContent,
    TagHelperContent InnerContent,
    string? ErrorMessage)
    : GovUkModelWithContent(InnerContent);