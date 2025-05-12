using IdentityChatEmail.Entities;
using IdentityChatEmail.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace IdentityChatEmail.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManger;

        public RegisterController(UserManager<AppUser> userManger)
        {
            _userManger = userManger;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterViewModel registerViewModel)
        {
            AppUser user = new AppUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                Name = registerViewModel.Name,
                Surname = registerViewModel.Surname,    
                Address = registerViewModel.Address
            };

            IdentityResult result = await _userManger.CreateAsync(user,registerViewModel.Password);
            if (result.Succeeded) { 
                return RedirectToAction("UserLogin","Login");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    //Eğer key bir property adıysa (örneğin "Email"), hata o input alanının altında (asp-validation-for="Email") görüntülenir.
                    //Eğer key boş string("") olarak verilirse, bu hata genel ModelOnly kısmında(asp-validation - summary = "ModelOnly") gösterilir.
                    //key, hatanın bağlandığı form alanını veya genel olup olmadığını belirliyor.

                    ModelState.AddModelError("",item.Description);
                }
            }
                return View();
        }
    }
}
