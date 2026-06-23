using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GovUk.AspNetCore.Mvc.Models;

public record GovUkAccordionSectionModel (
    string Caption,
    TagHelperContent InnerContent) : GovUkModelWithContent(InnerContent);