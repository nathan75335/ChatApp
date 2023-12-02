using ChatApp.Shared.DTO_s;
using ChatApp.Shared.Requests.Contact;
using ChatApp.Client.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatApp.Client.Desktop
{
    /// <summary>
    /// Interaction logic for ContactAddPage.xaml
    /// </summary>
    public partial class ContactAddPage : Page
    {
        private Frame MenuFrame { get; set; }
        public ContactAddPage(Frame menuFrame)
        {
            MenuFrame = menuFrame;
            InitializeComponent();
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

        private void txtUsername_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && txtUsername.Text.Length > 0)
                textUsernameBlock.Visibility = Visibility.Collapsed;
            else
                textUsernameBlock.Visibility = Visibility.Visible;
        }

        private void textUsernameBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUsername.Focus();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5107");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserManager.Token.Token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            var content = UserManager.GetContent(new ContactRequest
            {
                UserId = UserManager.Token.User.Id,
                Username = txtEmail.Text,
            });
            var response = await client.PostAsync($"users/contacts", content);

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Could add the contact, retry later");

                return;
            }

            MenuFrame.Content = new ContactPage(MenuFrame);
        }
    }
}
