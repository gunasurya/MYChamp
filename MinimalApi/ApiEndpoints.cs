using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MYChamp.Handlers;
using MYChamp.Models.Authentication;

namespace MYChamp.MinimalApi
{
    public static class ApiEndpoints
    {
        public static void MapEndpoints(this WebApplication app)
        {
            app.MapPost("/auth/register", RegisterUser)
               .WithName("RegisterUser")
               .Produces(StatusCodes.Status200OK)
               .Produces(StatusCodes.Status400BadRequest)
               .WithTags("Authentication")
               .WithSummary("Registers a new user.");

            app.MapPost("/auth/login", LoginUser)
              .WithName("LoginUser")
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .WithTags("Authentication")
              .WithSummary("Logs in a user.");
        }

        private static async Task<IResult> RegisterUser(
            [FromBody] Register registerModel,
            UserManager<ApplicationUser> userManager)
        {
            if (registerModel == null)
            {
                return Results.BadRequest("Invalid registration data.");
            }

            var user = new ApplicationUser
            {
                UserId = registerModel.UserId,
                UserName = registerModel.Email,
                Email = registerModel.Email,
                Address = registerModel.Address,
                Firstname = registerModel.FirstName,
                Middlename = registerModel.MiddleName,
                Lastname = registerModel.LastName
            };

            var result = await userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                return Results.Ok();
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Results.BadRequest($"Registration failed: {errors}");
        }
        private static async Task<IResult> LoginUser(
         [FromBody] Login login,
         SignInManager<ApplicationUser> signInManager,
         UserManager<ApplicationUser> userManager,
         IHttpContextAccessor httpContextAccessor,
         SessionHandler sessionHandler)
        {
            var sequenceId = httpContextAccessor.HttpContext.Session.GetString("sequence");
            httpContextAccessor.HttpContext.Session.SetString("sequence", Guid.NewGuid().ToString());

            if (login == null)
            {
                return Results.BadRequest("Invalid login data.");
            }

            var user = await userManager.FindByNameAsync(login.Name);
            if (user != null && await userManager.CheckPasswordAsync(user, login.Password))
            {
                var existingSession = sessionHandler.SessionExists(login.Name);
                if (existingSession)
                {
                    httpContextAccessor.HttpContext.Session.SetString("popUpShow", "true");
                    httpContextAccessor.HttpContext.Session.SetString("username", user.UserName);
                    httpContextAccessor.HttpContext.Session.SetString("password", login.Password);
                    return Results.Ok(new { Message = "Existing session found. Popup will be shown.", Code = 1 });
                }
                else
                {
                    var result = await signInManager.PasswordSignInAsync(login.Name, login.Password, login.RememberMe, false);
                    if (result.Succeeded)
                    {
                        var sessionId = httpContextAccessor.HttpContext.Session.Id;
                        var userId = login.Name;
                        var ipAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
                        var loginTime = DateTime.UtcNow;
                        sessionHandler.AddSessionInformation(sessionId, userId, ipAddress, loginTime);
                        return Results.Ok(new { Message = "Login successful.", Code = 2 });
                    }
                    else
                    {
                        return Results.BadRequest(new { Message = "Invalid login attempt.", Code = 3 });
                    }
                }
            }
            else
            {
                return Results.BadRequest(new { Message = "Invalid username or password.", Code = 4 });
            }
        }



    }
}
