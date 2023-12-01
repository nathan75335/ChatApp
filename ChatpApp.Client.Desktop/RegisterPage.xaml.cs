using ChatApp.Shared.Requests.User;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ChatpApp.Client.Desktop
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
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

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
           UserManager.GetMainWindow().MainFrame.Content = new LoginPage();
        }

        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
                textEmail.Visibility = Visibility.Collapsed;
            else
                textEmail.Visibility = Visibility.Visible;
        }

        private void textName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textName.Text) && textName.Text.Length > 0)
                textNameBlock.Visibility = Visibility.Collapsed;
            else
                textNameBlock.Visibility = Visibility.Visible;
        }

        private void txtUsername_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && txtUsername.Text.Length > 0)
                textUsernameBlock.Visibility = Visibility.Collapsed;
            else
                textUsernameBlock.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void textUsernameBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUsername.Focus();
        }

        private void textNameBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textName.Focus();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var client = UserManager.GetHttpClient();
            var content = UserManager.GetContent(new UserRequest
            {
                UserName = txtUsername.Text,
                Name = textName.Text,
                Email = txtEmail.Text,
                Password = passwordBox.Password
            });

            var response = await client.PostAsync("users", content);

            if(response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                MessageBox.Show("This email or username is alreay taken!");
                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Could not add the user");
                return;
            }

            UserManager.GetMainWindow().MainFrame.Content = new LoginPage();
        }
    }
}
