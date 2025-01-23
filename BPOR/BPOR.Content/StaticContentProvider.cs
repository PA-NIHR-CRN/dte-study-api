using AngleSharp.Io;
using Microsoft.AspNetCore.Localization;
using NIHR.GovUk.AspNetCore.Mvc.TagHelpers;
using NIHR.Infrastructure.Interfaces;
using System.Globalization;

internal class StaticContentProvider(IHttpContextAccessor httpContextAccessor) : IContentProvider
{

    public Task<TContent> GetContentAsync<TContent>(string contentId, CancellationToken cancellationToken = default) where TContent : new()
    {
        if (new TContent() is not RmsPage retval)
        {
            throw new NotImplementedException();
        }
        else
        {

            var rqf =  httpContextAccessor?.HttpContext?.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            var culture = rqf?.RequestCulture.Culture ?? CultureInfo.GetCultureInfo("en-GB");

            retval.Content = "sfgsdfs";

            return Task.FromResult((TContent)(object)retval);
        }
    }

    public async Task<TContent> GetContentAsync<TContent>(string contentId, string contentType, CancellationToken cancellationToken = default) where TContent : new()
    {
        return await GetContentAsync<TContent>(contentId, cancellationToken);
    }
}