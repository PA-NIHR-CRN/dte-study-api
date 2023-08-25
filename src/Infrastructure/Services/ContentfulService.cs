using System.Threading.Tasks;
using Application.Contracts;
using Contentful.Core;

namespace Infrastructure.Services;

public class ContentfulService: IContentfulService
{
    private readonly IContentfulClient _client;
    
    public ContentfulService(IContentfulClient client)
    {
        _client = client;
    }
    
    public async Task<string> GetContentfulEntry(string entryId)
    {
        var entry = await _client.GetEntry<dynamic>(entryId);
        return entry.ToString();
    }
    
}