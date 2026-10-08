namespace NIHR.GovUk.AspNetCore.Mvc.Models;

public record GovUkDateInputModel(
    string DayName,
    string DayId,
    string? DayValue,
    string MonthName,
    string MonthId,
    string? MonthValue,
    string YearName,
    string YearId,
    string? YearValue,
    string Legend,
    string? Hint,
    string? ErrorMessage,
    bool DayHasError,
    bool MonthHasError,
    bool YearHasError);