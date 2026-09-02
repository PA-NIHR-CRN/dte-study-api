using Microsoft.AspNetCore.Html;

namespace NIHR.GovUk.AspNetCore.Mvc.Models;

public record GovUkCheckboxModel(
    string Name,
    string Id,
    string? Label,
    IHtmlContent? LabelHtml,
    bool Checked,
    string? ErrorMessage);