using IdentityChatEmail.Context;
using IdentityChatEmail.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityChatEmail.ViewComponents
{
    public class SidebarViewComponentPartial:ViewComponent
    {
        private readonly EmailContext _emailContext;
        private readonly UserManager<AppUser> _userManager;
        public SidebarViewComponentPartial(EmailContext emailContext,UserManager<AppUser> userManager)
        {
            _emailContext = emailContext;
            _userManager = userManager; 
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var existUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (existUser != null) {
                ViewBag.inboxCount = _emailContext.Messages.Where(x => x.RecieverEmail == existUser.Email).Count();
                ViewBag.sendboxCount = _emailContext.Messages.Where(x => x.SenderEmail == existUser.Email).Count();
                ViewBag.messageCount = _emailContext.Messages.Count();

            }
            return View("Default");
        }
    }
}
