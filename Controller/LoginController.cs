using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MYChamp.AuthModel;
using MYChamp.DbContexts;
using MYChamp.Models;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace MYChamp.Controller
{
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly MYChampDbContext _db;
        private readonly SessionHandlerController _sessionHandlerController;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(MYChampDbContext db, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, SessionHandlerController sessionHandlerController, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _db = db;
            _signInManager = signInManager;
            _userManager = userManager;
            _sessionHandlerController = sessionHandlerController;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<ActionResult<int>> OnPostAsync(Login_model login_Model)
        {
            var sequenceId = _httpContextAccessor.HttpContext.Session.GetString("sequence"); // Access session value
            _httpContextAccessor.HttpContext.Session.SetString("sequence", Guid.NewGuid().ToString()); // Set session value

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(login_Model.Name);
                
                if (user != null && await _userManager.CheckPasswordAsync(user, login_Model.Password))
                {
                   MyChampIdentity myChampIdentity = new MyChampIdentity();
                   myChampIdentity.LoginId=user.Id;
                  



                    var existingSession = _sessionHandlerController.SessionExists(login_Model.Name);
                    Console.WriteLine(existingSession + "  " + login_Model.Name);

                    if (existingSession)
                    {
                        _httpContextAccessor.HttpContext.Session.SetString("popUpShow", "true");
                        _httpContextAccessor.HttpContext.Session.SetString("username", user.UserName);
                        _httpContextAccessor.HttpContext.Session.SetString("password", login_Model.Password);
                        return 1;
                    }
                    else
                    {
                        var result = await _signInManager.PasswordSignInAsync(login_Model.Name, login_Model.Password, login_Model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            var sessionId = _httpContextAccessor.HttpContext.Session.Id;
                            Console.WriteLine("session id ", sessionId);
                            var username = login_Model.Name;
                            var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();


                            var loginTime = DateTime.UtcNow;
                            _sessionHandlerController.AddSessionInformation(sessionId, username, ipAddress, loginTime,user.Id);
                            return 2; // Return appropriate response
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return 3;
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
            }
            return 4;
        }
    }
}
