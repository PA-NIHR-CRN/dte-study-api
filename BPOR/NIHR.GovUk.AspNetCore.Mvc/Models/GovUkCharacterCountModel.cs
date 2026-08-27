namespace NIHR.GovUk.AspNetCore.Mvc.Models;

public record GovUkCharacterCountModel(
    string Name,
    string Id,
    string? Label,
    string? Hint,
    string? Value,
    int MaxLength,
    int Rows,
    string? ErrorMessage);