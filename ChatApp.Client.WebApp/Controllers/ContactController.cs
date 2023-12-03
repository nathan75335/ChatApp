using Azure.Core;
using ChatApp.Shared.DTO_s;
using ChatApp.Shared.Requests.Contact;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ChatApp.Client.WebApp.Controllers;

public class ContactController : Controller
{
    public async Task<IActionResult> Index()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5107");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserManager.Token.Token);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        var response = await client.GetFromJsonAsync<List<ContactDto>>($"users/{UserManager.Token.User.Id}/contacts");
        
        return View(response);
    }

    public IActionResult Create()
    {
        return View();
    }

    public async Task<IActionResult> Create(ContactRequest request)
    {
        request.UserId = UserManager.Token.User.Id;
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserManager.Token.Token);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        var content = UserManager.GetContent(request);
        var response = await client.PostAsync("users/contacts", content);

        if(response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        ViewData["Error"] = "Could not add the contact";

        return View(response);
    }

    public async Task<IActionResult> Delete(int id)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserManager.Token.Token);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        var response = await client.DeleteAsync($"users/contacts/{id}");

        return RedirectToAction("Index");
    }
    
}
