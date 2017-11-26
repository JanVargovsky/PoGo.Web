using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PoGo.Web.Identity;
using PoGo.Web.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PoGo.Web.Controllers
{
    public class AccountController : Controller
    {
        readonly ILogger<AccountController> logger;
        readonly ExternalSignInManager<ApplicationUser> signInManager;

        public AccountController(ILogger<AccountController> logger, ExternalSignInManager<ApplicationUser> signInManager)
        {
            this.logger = logger;
            this.signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                logger.LogError($"Error from external provider: {remoteError}");
                return RedirectToAction("Login");
            }
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                logger.LogError($"Method GetExternalLoginInfoAsync did not return any info");
                return RedirectToAction("Login");
            }

            var userIdentity = info.Principal.Identity;
            ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
            //HttpContext.User = user; // doesnt work

            await HttpContext.SignInAsync(userPrincipal, new AuthenticationProperties
            {
                AllowRefresh = false,
                ExpiresUtc = DateTime.UtcNow.AddHours(12),
                IsPersistent = true,
            });
            System.Threading.Thread.CurrentPrincipal = HttpContext.User = userPrincipal;

            string url = returnUrl ?? Url.Page("/Index");
            return Redirect(url);
        }
    }
}
