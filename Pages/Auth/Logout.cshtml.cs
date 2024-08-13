using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MYChamp.Handlers;
using MYChamp.Models.Authentication;

namespace MYChamp.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly SessionHandler _sessionHandler;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, SessionHandler session)
        {
            _signInManager = signInManager;
            _sessionHandler = session;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var sessionId = HttpContext.Session.Id;
            var user = User.Identity.Name;

            _sessionHandler.UpdateSessionInformation(sessionId, user);
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Auth/Login");
        }
    }
}
