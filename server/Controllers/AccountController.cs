using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using server.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server.Controllers
{

    [Route("[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl)
        {
            var url = "http://localhost:3000/login?" + returnUrl;
            //Редирект на страницу аторизации
            return Redirect(url);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> PostLogin(string? returnUrl)
        {
            var form = Request.Form;

            if (!form.ContainsKey("login") || !form.ContainsKey("password"))
                return new BadRequestObjectResult("Email и/или пароль не установлены");

            string login = form["login"];
            string password = form["password"];

            User? user = await _userManager.FindByNameAsync(login);
            PasswordVerificationResult coparePasswordsResults = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (user == null || coparePasswordsResults == PasswordVerificationResult.Failed)
            {
                return new UnauthorizedObjectResult("Неверный логин или пароль");
            }
            else
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return (
                    returnUrl is not null
                        ? new RedirectResult(returnUrl)
                        : new OkObjectResult("Авторизация прошла успешно")
                );
            }
        }
    }
}
