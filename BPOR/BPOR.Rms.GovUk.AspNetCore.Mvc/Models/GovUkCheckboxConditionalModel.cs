using Microsoft.AspNetCore.Html;

namespace NIHR.GovUk.AspNetCore.Mvc.Models;

public record GovUkCheckboxConditionalModel(
    string Id,
    bool IsHidden,
    IHtmlContent InnerContent);