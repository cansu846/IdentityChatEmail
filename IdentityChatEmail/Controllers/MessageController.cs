using IdentityChatEmail.Context;
using IdentityChatEmail.Entities;
using IdentityChatEmail.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityChatEmail.Controllers
{
    [Route("Message/[action]")]
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
        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> Sendbox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user != null)
            {
                ViewBag.Email = user.Email;
                ViewBag.FullName = user.Name + " " + user.Surname;
                var senbox = _emailContext.Messages.Where(x => x.SenderEmail == user.Email).ToList();
                //var inbox = _emailContext.Messages.ToList();

                return View(senbox);
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageViewModel messageViewModel)
        {
            var userLogin = await _userManager.FindByNameAsync(User.Identity.Name);
            Message message = new Message {
                SenderEmail = userLogin.Email,
                RecieverEmail = messageViewModel.RecieverEmail,
                Subject = messageViewModel.Subject,
                MessageDetail = messageViewModel.MessageDetail,
                SendDate = DateTime.Now
            };
            _emailContext.Messages.Add(message);
            _emailContext.SaveChanges();
            return RedirectToAction("Sendbox", "Message");
        }
    }
}
