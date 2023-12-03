using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Client.WebApp.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
