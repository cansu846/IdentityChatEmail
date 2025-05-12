using IdentityChatEmail.Context;
using IdentityChatEmail.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityChatEmail.Controllers
{
    public class MessageController : Controller
    {
        private readonly EmailContext _emailContext;
        private readonly UserManager<AppUser> _userManager;

        public MessageController(EmailContext emailContext, UserManager<AppUser> userManager)
        {
            _emailContext = emailContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Inbox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            
            if (user!=null)
            {
                ViewBag.Email = user.Email;
                ViewBag.FullName = user.Name + " " + user.Surname;
                var inbox = _emailContext.Messages.Where(x => x.RecieverEmail == user.Email).ToList();
                //var inbox = _emailContext.Messages.ToList();

                return View(inbox);
            }
            return View();  
        }
    }
}
