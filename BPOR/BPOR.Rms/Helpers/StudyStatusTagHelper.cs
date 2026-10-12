using BPOR.Domain.Enums;
using NIHR.GovUk.AspNetCore.Mvc.Models;

namespace BPOR.Rms.Helpers;

public static class StudyStatusTagHelper
{
    public static GovUkTagColour GetTagColour(StudyStatusType status) =>
        status switch
        {
            StudyStatusType.NewApplication => GovUkTagColour.Grey,
            StudyStatusType.InProgress => GovUkTagColour.Blue,
            StudyStatusType.ConcludedSuccessfully => GovUkTagColour.Purple,
            StudyStatusType.Withdrawn => GovUkTagColour.Yellow,
            StudyStatusType.Active => GovUkTagColour.Green,
            StudyStatusType.Rejected => GovUkTagColour.Red,
            _ => GovUkTagColour.Grey
        };
}