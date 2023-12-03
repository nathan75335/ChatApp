using ChatApp.Shared.DTO_s;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ChatApp.Client.WebApp;

public class UserManager
{
    public static TokenDto Token { get; set; }   
    public static StringContent GetContent<T>(T request)
    {
        var dataString = JsonSerializer.Serialize<T>(request);
        var content = new StringContent(dataString);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return content;
    }

    public static StringContent GetContentWithJwt<T>(T request)
    {
        var dataString = JsonSerializer.Serialize<T>(request);
        var content = new StringContent(dataString);
        content.Headers.Add("Authorization", $"Bearer {Token.Token}");
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return content;
    }

    public static StringContent GetNoContentWithJwt()
    {
        var content = new StringContent("");
        content.Headers.Add("Authorization", $"Bearer {Token.Token}");
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return content;
    }
}
