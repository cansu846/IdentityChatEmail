using IdentityChatEmail.Context;
using IdentityChatEmail.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace IdentityChatEmail.Controllers
{
    public class ProfileController : Controller
    {
        private readonly EmailContext _emailContext;    
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(EmailContext emailContext, UserManager<AppUser> userManager)
        {
            _emailContext = emailContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyProfile()
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.fullName = value.Name + " " + value.Surname;
            ViewBag.email = value.Email;
            ViewBag.phone = value.PhoneNumber;
            ViewBag.image = value.ProfileImage;
            ViewBag.address  = value.Address;
            return View();
        }
    }
}
