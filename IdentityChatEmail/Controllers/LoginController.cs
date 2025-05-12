using IdentityChatEmail.Entities;
using IdentityChatEmail.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace IdentityChatEmail.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManger;

        public LoginController(SignInManager<AppUser> signInManger)
        {
            _signInManger = signInManger;
        }

        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManger.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, true, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Message");
                }
                else
                {
                    ModelState.AddModelError("","Username or password not valid. Please try again...");

                }
            }
                return View();
        }
    }
}
