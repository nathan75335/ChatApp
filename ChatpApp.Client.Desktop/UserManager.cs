using ChatApp.Shared.DTO_s;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Windows;

namespace ChatpApp.Client.Desktop;

public class UserManager
{
    public static TokenDto Token { get; set; }
    public static HttpClient Client { get; set; } = new HttpClient();

    static UserManager() 
    {
        Client.BaseAddress = new Uri("http://localhost:5107");
    }


    public static MainWindow GetMainWindow()
    {
        return Application.Current.MainWindow as MainWindow;
    }

    public static HttpClient GetHttpClient()
    {
        return Client;
    }

    public static StringContent GetContent<T>(T request)
    {
        var dataString = JsonSerializer.Serialize<T>(request);
        var content = new StringContent(dataString);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return content;
    }
}
