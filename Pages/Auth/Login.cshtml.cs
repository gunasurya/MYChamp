using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MYChamp.AuthModel;
using MYChamp.DbContexts;
using MYChamp.Models;


namespace MYChamp.Pages.Auth
{

    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login_model login_Model { get; set; }
        private readonly SignInManager<AppUser> _signInManager;

        private readonly MYChampDbContext _db;
        public LoginModel(MYChampDbContext db,SignInManager<AppUser> signinmanager)
        {
            _signInManager = signinmanager;
            _db = db;
            login_Model = new Login_model();
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login_Model.Name, login_Model.Password, login_Model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }
            return Page();
        }

    }
}
