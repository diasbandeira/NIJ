using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NIJ.Web.Models.Infra;
using NIJ.Web.Models.Infra.ViewModel;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace NIJ.Web.Controllers
{
    [Authorize]
    public class InfraController : Controller
    {
        private readonly UserManager<UserAplication> _userManager;
        private readonly SignInManager<UserAplication> _signInManager;
        private readonly ILogger _logger;

        public InfraController(UserManager<UserAplication> userManager, SignInManager<UserAplication> signInManager, ILogger<InfraController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Acessar(string returnUrl)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            
            return View();

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterNewUser(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterNewUser(RegisterNewUserViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new UserAplication { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    _logger.LogInformation("Usuario criou uma nova senha");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("Usuario acessou com a conta criada");

                    return RedirectToLocal(returnUrl);
                }
                
                AddErrors(result);
            }
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
            