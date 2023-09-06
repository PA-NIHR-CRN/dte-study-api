using Application.Responses.V1;

namespace Application.Contracts;

public interface IRichTextToHtmlConverter
{
    string Convert(RichTextNode richTextNode);
}