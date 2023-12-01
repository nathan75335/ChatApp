using ChatApp.Shared.DTO_s;
using System.Windows;

namespace ChatpApp.Client.Desktop;

public class UserManager
{
    public static TokenDto Token { get; set; }

    public static MainWindow GetMainWindow()
    {
        return Application.Current.MainWindow as MainWindow;
    }
}
