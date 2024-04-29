using BLL.DTO;
using DAL.Entities;
using Kyrsach.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace Kyrsach.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByNameAsync(model.Username);
        //        var roles = await _userManager.GetRolesAsync(user);
        //        var role = roles.FirstOrDefault();
        //        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        //        {
        //            var claims = new List<Claim>{
        //                    new Claim(ClaimTypes.Name, user.UserName),
        //                    new Claim(ClaimTypes.Role, role), 
        //                    // Другие данные пользователя, которые вы хотите сохранить
        //               };
        //            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //            var authProperties = new AuthenticationProperties
        //            {
        //                // Дополнительные свойства аутентификации, например, время жизни куки и др.
        //                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30), // Установите желаемый срок действия куки
        //                IsPersistent = true // Сделайте куки постоянными (не сессионными)
        //            };

        //            await HttpContext.SignInAsync(
        //                CookieAuthenticationDefaults.AuthenticationScheme,
        //                new ClaimsPrincipal(claimsIdentity),
        //                authProperties);

        //            return RedirectToAction("Index", "Home");
        //        }
        //        ModelState.AddModelError("", "Invalid login attempt.");
        //    }
        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: false);
                var user = await _userManager.FindByNameAsync(model.Username);
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();
                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("UserName", model.Username);
                    HttpContext.Session.SetString("UserRole", role);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username, Email = model.Email, PhoneNumber = model.PhoneNumber };

                // Создаем роль "Клиент", если ее нет
                var roleExists = await _roleManager.RoleExistsAsync("Клиент");
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Клиент"));
                }

                // Регистрируем пользователя и назначаем ему роль "Клиент"
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Клиент");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
