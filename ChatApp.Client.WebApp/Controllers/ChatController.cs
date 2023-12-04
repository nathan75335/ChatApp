using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Client.WebApp.Controllers
{
   
    public class ChatController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AdminPage()
        {
            return View();
        }
    }
}
