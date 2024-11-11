using System.Collections.Generic;
using System.Text.Json;
using Amazon.SimpleEmail.Model;
using BPOR.Domain.Entities;
using BPOR.Infrastructure.Clients;
using BPOR.Rms.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NIHR.Infrastructure;
using NIHR.Infrastructure.Models;
using Rbec.Postcodes;
using Z.EntityFramework.Plus;

namespace BPOR.Rms.TagHelpers
{
    public class AddressLookupTagHelper : PartialTagHelper
    {


        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }


        private readonly IHtmlGenerator _generator;
        private readonly IPostcodeMapper _locationApiClient;
        private readonly ILogger _logger;

        public string postcode { get; set; }
        public bool autoFocus { get; set; }


        /*  notes
         *  
         *  input specific tag helper, made to be nested within the formgroup tag helper.
         *  
         *  jsonify the value to hold all data? display only the full address.
         */
        public AddressLookupTagHelper(IPostcodeMapper locationApiClient, ICompositeViewEngine viewEngine, IViewBufferScope viewBufferScope, IHtmlGenerator generator, ILogger<AddressLookupTagHelper> logger) : base(viewEngine, viewBufferScope)
        {
            _locationApiClient = locationApiClient;
            _logger = logger;
            _generator = generator;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            IEnumerable<PostcodeAddressModel> addressModels;
            addressModels = await _locationApiClient.GetAddressesByPostcodeAsync(postcode, new CancellationToken());

            if (addressModels.Count() == 0)
            {
                ViewContext.ModelState.AddModelError(For.Name, "We cannot find a match for the postcode entered. Please try again or enter your address manually.");
                _logger.LogWarning("No addresses found for post code : " + postcode);
                addressModels = new List<PostcodeAddressModel>();
            }
            string selectMessage = addressModels.Count() == 0 ? "No addresses found" : $"{addressModels.Count()} addresses found";
            var selectListItems = addressModels.Select(x => new SelectListItem { Text = x.FullAddress, Value = JsonSerializer.Serialize(x) });
            var SelectTag = _generator.GenerateSelect(ViewContext, For.ModelExplorer, selectMessage, For.Name, selectListItems, false, new
            {
                @class = "govuk-select govuk-select-custom",
                @autofocus = autoFocus
            });


            output.PreContent.AppendHtml(SelectTag);





        }
    }
}
