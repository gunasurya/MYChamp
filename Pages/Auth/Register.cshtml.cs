using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MYChamp.AuthModel;
using MYChamp.Models;
using MYChamp.DbContexts;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace MYChamp.Pages.Auth
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly MYChampDbContext _dbContext;
        public Register_Model _register {  get; set; }

        public RegisterModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, MYChampDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _register = new Register_Model();
        }

        protected readonly Register_Model _register_model;

       

        public IActionResult OnGet()
        {
            return Page();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {

                var user = new AppUser
                {
                    UserName = _register.Email,
                    Email = _register.Email,
                    address = _register.Address,
                    name= _register.Name,

                };

                var result = await _userManager.CreateAsync(user, _register.Password);

                if (result.Succeeded)
                {
                    
                  //  await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToPage("/Auth/Login");
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }



            return Page();
        }
    }
}
