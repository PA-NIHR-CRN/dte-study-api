namespace NIHR.GovUk.AspNetCore.Mvc.Models;

public record GovUkTextAreaModel(
    string Name,
    string Id,
    string? Value,
    int MaxLength,
    int Rows,
    string? ErrorMessage);