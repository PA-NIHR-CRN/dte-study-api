using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NIHR.Infrastructure.Models;

namespace BPOR.Rms.TagHelpers
{
    /// <summary>
    /// Renders a list of addresses in a <select> element for the user to pick from.
    /// Example usage:
    /// <address-lookup for="SelectedAddress" addresses="@Model.Addresses" auto-focus="true" />
    /// </summary>
    [HtmlTargetElement("address-lookup", Attributes = "for, addresses")]
    public class AddressLookupTagHelper : PartialTagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }

        private readonly IHtmlGenerator _generator;
        private readonly ILogger _logger;

        [HtmlAttributeName("postcode")]
        public string postcode { get; set; }

        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("auto-focus")]
        public bool AutoFocus { get; set; }

        [HtmlAttributeName("addresses")]
        public List<PostcodeAddressModel>? Addresses { get; set; }

        /*  notes
         *  
         *  input specific tag helper, made to be nested within the formgroup tag helper.
         *  
         *  jsonify the value to hold all data? display only the full address.
         */
        public AddressLookupTagHelper(ICompositeViewEngine viewEngine, IViewBufferScope viewBufferScope, IHtmlGenerator generator, ILogger<AddressLookupTagHelper> logger) : base(viewEngine, viewBufferScope)
        {
            _logger = logger;
            _generator = generator;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (Addresses == null || Addresses.Count == 0)
            {
                _logger.LogWarning("No addresses to display for property: {ForName}. Postcode: {Postcode}",
                                   For.Name, postcode);
                Addresses = new List<PostcodeAddressModel>();
            }

            string selectMessage = Addresses.Count() == 0 ? "No addresses found" : $"{Addresses.Count()} addresses found";

            var selectListItems = Addresses.Select(x => new SelectListItem
            {
                Text = x.FullAddress,
                Value = JsonSerializer.Serialize(x)
            });

            var SelectTag = _generator.GenerateSelect(ViewContext, For.ModelExplorer, selectMessage, For.Name, selectListItems, false, new
            {
                @class = "govuk-select govuk-select-custom",
                @autofocus = AutoFocus
            });


            output.PreContent.AppendHtml(SelectTag);

        }
    }
}
