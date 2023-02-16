using System.Threading.Tasks;
using Application.Extensions;
using Application.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StudyApi.Attributes
{
    public class AuthorizeResearcherAttribute : TypeFilterAttribute
    {
        public AuthorizeResearcherAttribute() : base(typeof(AuthorizeResearcherFilter)) { }
    }

    public class AuthorizeResearcherFilter : IAsyncActionFilter
    {
        private readonly IdentitySettings _identitySettings;

        public AuthorizeResearcherFilter(IdentitySettings identitySettings)
        {
            _identitySettings = identitySettings;
        }
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var isExternalProviderLogin = context.HttpContext.User.IsExternalProviderUser(_identitySettings.IdgExternalProviderName);

            if (!isExternalProviderLogin)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            
            await next();
        }
    }
}