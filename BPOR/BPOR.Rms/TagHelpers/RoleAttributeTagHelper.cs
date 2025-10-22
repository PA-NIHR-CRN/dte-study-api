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
        public string? RoleDisable { get; set; }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Process(currentUserProvider, output, RoleInclude, RoleExclude, RoleDisable);
        }

        public static void Process(ICurrentUserProvider<User> currentUserProvider, TagHelperOutput output, string? roleInclude, string? roleExclude, string? roleDisable)
        {
            if (string.Equals(output.TagName, "role", StringComparison.InvariantCultureIgnoreCase))
            {
                output.TagName = null;
            }

            var include = roleInclude?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? [];
            var exclude = roleExclude?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? [];
            var disable = roleDisable?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? [];
            
            if (exclude.Any(currentUserProvider.User.HasRole))
            {
                output.SuppressOutput();
            }
            else if (include.Any() && !include.Any(currentUserProvider.User.HasRole))
            {
                output.SuppressOutput();
            }
            
            if (disable.Any(currentUserProvider.User.HasRole))
            {
                output.Attributes.Add("disabled", "disabled");
                output.Attributes.Add("tabindex", "-1");
            }

        }
    }

    public class RoleTagHelper(ICurrentUserProvider<User> currentUserProvider) : TagHelper
    {
        public string? Include { get; set; }
        public string? Exclude { get; set; }
        public string? Disable { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            RoleAttributeTagHelper.Process(currentUserProvider, output, Include, Exclude, Disable);
        }
    }
}
