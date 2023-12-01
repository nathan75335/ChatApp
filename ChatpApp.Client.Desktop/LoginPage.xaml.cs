using ChatApp.Shared.DTO_s;
using ChatApp.Shared.Requests.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatpApp.Client.Desktop
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private HttpClient _httpClient;
        public LoginPage()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5107");
            InitializeComponent();

        }

        private void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            UserManager.GetMainWindow().MainFrame.Content = new RegisterPage();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                UserManager.GetMainWindow().DragMove(); ;
            }

        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(passwordBox.Password))
            {
                MessageBox.Show("Successfully Signed In");
            }
        }

        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
                textEmail.Visibility = Visibility.Collapsed;
            else
                textEmail.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(passwordBox.Password))
            {
                MessageBox.Show("Enter the email and the password!");

                return;
            }

            var dataAsString = JsonSerializer.Serialize(new UserLoginRequest()
            {
                UserNameOrEmail = txtEmail.Text,
                Password = passwordBox.Password
            });
            var content = new StringContent(dataAsString);
            var contentType = new MediaTypeHeaderValue("application/json");
            content.Headers.ContentType = contentType;
            var response = await _httpClient.PostAsync("auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Could not login to the chat app, retry later");

                return;
            }

            UserManager.Token = await response.Content.ReadFromJsonAsync<TokenDto>();
            UserManager.GetMainWindow().MainFrame.Content = new MainPage();
        }

    }
}
