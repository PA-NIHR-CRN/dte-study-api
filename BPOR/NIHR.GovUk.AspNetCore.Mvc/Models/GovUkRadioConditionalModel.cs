using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GovUk.AspNetCore.Mvc.Models;

public record GovUkRadioConditionalModel(
    string Id,
    bool IsHidden,
    TagHelperContent InnerContent)
        : GovUkModelWithContent(InnerContent);