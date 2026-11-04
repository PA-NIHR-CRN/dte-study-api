using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GovUk.AspNetCore.Mvc.Models;

public record GovUkFieldSetModel (
    string HeadingText,
    GovUkHeadingSize HeadingSize, 
    TagHelperContent InnerContent) 
    : GovUkModelWithContent(InnerContent);
    