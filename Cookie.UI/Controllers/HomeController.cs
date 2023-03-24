using Cookie.UI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Cookie.UI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Cookie.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly CookieContext context;

        public HomeController(CookieContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInModel model)
        {
            var user = context.Users.SingleOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

            if (user != null)
            {
                var role = context.Roles.Where(x => x.UserRoles.Any(x => x.UserId == user.Id)).Select(x => x.Definiton).ToList();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                };
                foreach (var r in role)
                {
                    claims.Add(new Claim(ClaimTypes.Role, r));
                }

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.Remember
                };
                await HttpContext.SignInAsync(
               CookieAuthenticationDefaults.AuthenticationScheme,
               new ClaimsPrincipal(claimsIdentity),
               authProperties);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");

        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }
        [Authorize(Roles = "Member")]
        public IActionResult Member()
        {
            return View();
        }
    }
}
