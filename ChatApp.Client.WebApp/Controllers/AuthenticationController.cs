using ChatApp.Shared.DTO_s;
using ChatApp.Shared.Requests.User;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace ChatApp.Client.WebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequest loginRequest)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5107");
            var content = UserManager.GetContent(loginRequest);
            var response = await client.PostAsync("auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                UserManager.Token = await response.Content.ReadFromJsonAsync<TokenDto>();

                return RedirectToAction("Index", "Home");
            }

            ViewData["Error"] = "Could not login in the application";

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRequest userRequest)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5107");
            var content = UserManager.GetContent(userRequest);
            var response = await client.PostAsync("users", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

            ViewData["Error"] = "Something went wrong try again later";

            return View();
        }
    }
}
