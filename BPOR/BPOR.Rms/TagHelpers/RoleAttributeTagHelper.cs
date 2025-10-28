using BPOR.Domain.Entities;
using BPOR.Rms.Startup;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BPOR.Rms.TagHelpers
{
    [HtmlTargetElement(Attributes = "role-exclude")]
    [HtmlTargetElement(Attributes = "role-include")]
    [HtmlTargetElement(Attributes = "role-disable")]
    public class RoleAttributeTagHelper(ICurrentUserProvider<User> currentUserProvider) : TagHelper
    {
        public string? RoleInclude { get; set; }
        public string? RoleExclude { get; set; }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Process(currentUserProvider, output, RoleInclude, RoleExclude);
        }

        public static void Process(ICurrentUserProvider<User> currentUserProvider, TagHelperOutput output, string? roleInclude, string? roleExclude)
        {
            if (string.Equals(output.TagName, "role", StringComparison.InvariantCultureIgnoreCase))
            {
                output.TagName = null;
            }

            var include = roleInclude?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? [];
            var exclude = roleExclude?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? [];
            
            if (exclude.Any(currentUserProvider.User.HasRole))
            {
                output.SuppressOutput();
            }
            else if (include.Any() && !include.Any(currentUserProvider.User.HasRole))
            {
                output.SuppressOutput();
            }
        }
    }

    public class RoleTagHelper(ICurrentUserProvider<User> currentUserProvider) : TagHelper
    {
        public string? Include { get; set; }
        public string? Exclude { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            RoleAttributeTagHelper.Process(currentUserProvider, output, Include, Exclude);
        }
    }
}
