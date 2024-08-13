using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MYChamp.Handlers;
using MYChamp.Models.Authentication;

namespace MYChamp.Pages.Auth
{
    public class Forceful_logoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly SessionHandler _sessionHandler;

        public Forceful_logoutModel(SignInManager<ApplicationUser> signInManager, SessionHandler session)
        {
            _signInManager = signInManager;
            _sessionHandler = session;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var sessionId = HttpContext.Session.Id;
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            var username = HttpContext.Session.GetString("username");
            _sessionHandler.UpdateForceLogout(username, username + ipAddress);
            var password = HttpContext.Session.GetString("password");
            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);
            if (result.Succeeded)
            {
                var loginTime = DateTime.UtcNow;
                _sessionHandler.AddSessionInformation(sessionId, username, ipAddress, loginTime);
                bool value = _sessionHandler.check(HttpContext.Session.GetString("UniqueId"));
                if (value)
                {
                    await _signInManager.SignOutAsync();
                }
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                return Page();
            }

        }
    }
}
