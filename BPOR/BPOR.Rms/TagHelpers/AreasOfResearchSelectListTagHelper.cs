using BPOR.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Z.EntityFramework.Plus;

namespace BPOR.Rms.TagHelpers
{
    public class AreasOfResearchSelectListTagHelper : PartialTagHelper
    {
        private readonly IHtmlGenerator _generator;
        private IEnumerable<SelectListItem> _healthConditions;

        public AreasOfResearchSelectListTagHelper(ParticipantDbContext dbContext, ICompositeViewEngine viewEngine, IViewBufferScope viewBufferScope, IHtmlGenerator generator) : base(viewEngine, viewBufferScope)
        {
            _healthConditions = dbContext.HealthConditions
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description })
                .OrderBy(x => x.Text)
                .Future();
            _generator = generator;
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            Name = "Partials/_AreasOfResearch";

            ViewContext.ViewData["__AreasOfResearch"] = _healthConditions;

            ViewContext.ViewData["__AreasOfResearch_DisplayName"] = For.Metadata.DisplayName;

            ViewContext.ViewData["__AreasOfResearch_CurrentValues"] = _generator.GetCurrentValues(ViewContext, For.ModelExplorer, For.Name, allowMultiple: true);

            return base.ProcessAsync(context, output);
        }
    }
}
