using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Studies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Attributes;
using StudyApi.Common;

namespace StudyApi.Controllers.V1.Tokens
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/token")]
    public class TokenController : Controller
    {
        [Authorize("AnyAuthenticatedUser")]
        [HttpGet]
        public async Task<IActionResult> Default()
        {
            var allowedScope = User.HasClaim("scope", "openid profile email http://localhost:3000/token.parse");
            return await Task.FromResult(Ok(new
            {
                User.Identity.IsAuthenticated, 
                Claims = User.Claims.Select(x => new {x.Type, x.Value})
            }));
        }
        
        [Authorize(AppRoles.Admin)]
        [HttpGet("admin")]
        public async Task<IActionResult> Index()
        {
            var allowedScope = User.HasClaim("scope", "openid profile email http://localhost:3000/token.parse");
            var isInRole = User.IsInRole(AppRoles.Admin);
            return await Task.FromResult(Ok(new
            {
                User.Identity.IsAuthenticated, 
                Claims = User.Claims.Select(x => new {x.Type, x.Value})
            }));
        }
        
        [Authorize(AppRoles.Lead)]
        [HttpGet("lead")]
        public async Task<IActionResult> Lead()
        {
            var allowedScope = User.HasClaim("scope", "openid profile email http://localhost:3000/token.parse");
            var isInRole = User.IsInRole("Lead");
            return await Task.FromResult(Ok(new
            {
                User.Identity.IsAuthenticated, 
                Claims = User.Claims.Select(x => new {x.Type, x.Value})
            }));
        }
        
        [HttpGet("researcher"), AuthorizeResearcher]
        public async Task<IActionResult> Researcher()
        {
            var allowedScope = User.HasClaim("scope", "openid profile email http://localhost:3000/token.parse");
            return await Task.FromResult(Ok(new
            {
                User.Identity.IsAuthenticated, 
                Claims = User.Claims.Select(x => new {x.Type, x.Value})
            }));
        }
        
        [HttpGet("researcher/{studyId:long}"), AuthorizeResearcher]
        public async Task<IActionResult> Researcher(long studyId)
        {
            var allowedScope = User.HasClaim("scope", "openid profile email http://localhost:3000/token.parse");
            return await Task.FromResult(Ok(new
            {
                User.Identity.IsAuthenticated, 
                Claims = User.Claims.Select(x => new {x.Type, x.Value})
            }));
        }
    }
}