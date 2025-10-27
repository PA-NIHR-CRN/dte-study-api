using BPOR.Content.Models.Components;
using Contentful.AspNetCore.TagHelpers;
using Contentful.Core.Models;

namespace BPOR.Content
{
    public class CookiePolicyPage
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }

        public List<IContentfulComponent> pageSections {  get; set; }

    }


}
