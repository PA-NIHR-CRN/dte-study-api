using Amazon.SimpleEmail.Model;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace NIHR.Infrastructure.AspNetCore
{
    public abstract class GovUkBaseRenderer : IContentRenderer
    {
        private readonly ContentRendererCollection _rendererCollection;

        protected GovUkBaseRenderer(ContentRendererCollection rendererCollection)
        {
            _rendererCollection = rendererCollection;
        }

        public int Order { get; set; } = 10;

        public abstract Task<string> RenderAsync(IContent content);

        public abstract bool SupportsContent(IContent content);

        protected async Task<string> GetInnerHtmlAsync(List<IContent> contents)
        {
            StringBuilder sb = new();

            foreach (IContent item in contents)
            {
                IContentRenderer rendererForContent = _rendererCollection.GetRendererForContent(item);
                StringBuilder stringBuilder = sb;
                stringBuilder.Append(await rendererForContent.RenderAsync(item));
            }

            return sb.ToString();
        }

        protected string RenderTagBuilder(TagBuilder tagBuilder)
        {
            using var sw = new StringWriter();
            tagBuilder.WriteTo(sw, HtmlEncoder.Default);
            return sw.ToString();
        }
    }
}
