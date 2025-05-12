using Microsoft.AspNetCore.Mvc;

namespace IdentityChatEmail.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
