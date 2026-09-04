using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NIHR.GovUk.AspNetCore.Mvc.Models;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers;

[HtmlTargetElement("date-input")]
public class DateInputTagHelper(IHtmlHelper htmlHelper)
    : PartialTagHelperBase(htmlHelper)
{
    [HtmlAttributeName("day-for")]
    public ModelExpression DayFor { get; set; } = null!;

    [HtmlAttributeName("month-for")]
    public ModelExpression MonthFor { get; set; } = null!;

    [HtmlAttributeName("year-for")]
    public ModelExpression YearFor { get; set; } = null!;

    public string Legend { get; set; } = string.Empty;

    public string? Hint { get; set; }

    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        output.TagName = null;

        var dayName = GetFullName(DayFor);
        var monthName = GetFullName(MonthFor);
        var yearName = GetFullName(YearFor);

        var dayId = TagBuilder.CreateSanitizedId(dayName, "_");
        var monthId = TagBuilder.CreateSanitizedId(monthName, "_");
        var yearId = TagBuilder.CreateSanitizedId(yearName, "_");

        var dayErrors = GetErrors(dayName);
        var monthErrors = GetErrors(monthName);
        var yearErrors = GetErrors(yearName);

        var errorMessage = dayErrors
            .Concat(monthErrors)
            .Concat(yearErrors)
            .FirstOrDefault();

        var model = new GovUkDateInputModel(
            DayName: dayName,
            DayId: dayId,
            DayValue: GetValue(dayName, DayFor),
            MonthName: monthName,
            MonthId: monthId,
            MonthValue: GetValue(monthName, MonthFor),
            YearName: yearName,
            YearId: yearId,
            YearValue: GetValue(yearName, YearFor),
            Legend: Legend,
            Hint: Hint,
            ErrorMessage: errorMessage,
            DayHasError: dayErrors.Count > 0,
            MonthHasError: monthErrors.Count > 0,
            YearHasError: yearErrors.Count > 0);

        var content = await RenderPartialAsync(
            "_DateInput",
            model);

        output.Content.SetHtmlContent(content);
    }

    private string GetFullName(ModelExpression expression)
    {
        return ViewContext.ViewData.TemplateInfo
            .GetFullHtmlFieldName(expression.Name);
    }

    private string? GetValue(
        string fullName,
        ModelExpression expression)
    {
        if (ViewContext.ViewData.ModelState.TryGetValue(
                fullName,
                out var modelState))
        {
            var attemptedValue = modelState.AttemptedValue;

            if (!string.IsNullOrWhiteSpace(attemptedValue))
            {
                return attemptedValue;
            }
        }

        return expression.Model?.ToString();
    }

    private IReadOnlyList<string> GetErrors(string fullName)
    {
        if (!ViewContext.ViewData.ModelState.TryGetValue(
                fullName,
                out var modelState))
        {
            return [];
        }

        return modelState.Errors
            .Select(error => error.ErrorMessage)
            .Where(message => !string.IsNullOrWhiteSpace(message))
            .ToList();
    }
}