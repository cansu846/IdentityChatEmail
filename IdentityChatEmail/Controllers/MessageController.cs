using IdentityChatEmail.Context;
using IdentityChatEmail.Entities;
using IdentityChatEmail.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IdentityChatEmail.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
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
                var senbox = _emailContext.Messages.Where(x => x.SenderEmail != null && x.SenderEmail == user.Email).ToList() ;
                var inbox = _emailContext.Messages.ToList();

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
                SenderFullName = userLogin.Name + " " + userLogin.Surname,
                RecieverEmail = messageViewModel.RecieverEmail,
                RecieverFullName = messageViewModel.RecieverFullName,
                Subject = messageViewModel.Subject,
                MessageDetail = messageViewModel.MessageDetail,
                SendDate = DateTime.Now
            };
            _emailContext.Messages.Add(message);
            _emailContext.SaveChanges();
            TempData["SuccessMessage"] = "Message send successfully!";
            //return RedirectToAction("Sendbox", "Message");
            return View();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> MessageDetail(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.senderEmail = user.Email;
            var existMessage = _emailContext.Messages.Find(id);
            
            return View(existMessage);
        }

        [HttpGet]
        public async Task<IActionResult> SearchMessageForInbox(string searchTerm)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return Unauthorized();

            var query = _emailContext.Messages
                .Where(m => m.RecieverEmail == user.Email);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(m => m.Subject.Contains(searchTerm) || m.SenderEmail.Contains(searchTerm));
            }

            var filteredMessages = query
                .OrderByDescending(m => m.SendDate)
                .ToList();

            return PartialView("_MessageTableInboxPartial", filteredMessages);
        }

        [HttpGet]
        public async Task<IActionResult> SearchMessageForSendbox(string searchTerm)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return Unauthorized();

            var query = _emailContext.Messages
                .Where(m => m.SenderEmail == user.Email);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(m => m.Subject.Contains(searchTerm) || m.RecieverEmail.Contains(searchTerm));
            }

            var filteredMessages = query
                .OrderByDescending(m => m.SendDate)
                .ToList();

            return PartialView("_MessageTableSendboxPartial", filteredMessages);
        }

    }
}
