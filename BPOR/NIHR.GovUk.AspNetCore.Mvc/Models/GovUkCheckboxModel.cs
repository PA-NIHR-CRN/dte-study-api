namespace NIHR.GovUk.AspNetCore.Mvc.Models;

public record GovUkCheckboxModel(
    string Name,
    string Id,
    string? Label,
    bool Checked,
    string? ErrorMessage);