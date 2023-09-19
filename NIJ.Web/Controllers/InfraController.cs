using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NIJ.Web.Models.Infra;
//using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace NIJ.Web.Controllers
{
    [Authorize]
    public class InfraController : Controller
    {
        private readonly UserManager<UserAplication> _userManager;
        private readonly SignInManager<UserAplication> _signInManager;
        //private readonly ILogger _logger;

        public InfraController(UserManager<UserAplication> userManager, SignInManager<UserAplication> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Acessar(string returnUrl)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            
            return View();

        }
    }
}
