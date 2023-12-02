using ChatApp.Shared.DTO_s;
using ChatApp.Client.Desktop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ContactPage.xaml
    /// </summary>
    public partial class ContactPage : Page
    {
        public ObservableCollection<ContactDto> Contacts { get; set; }
        private  Frame MenuFrame { get; set; }

        public ContactPage(Frame menuFrame)
        {
            InitializeComponent();
            DataContext = this;

            // Initialize the ObservableCollection
            Contacts = new ObservableCollection<ContactDto>();
            MenuFrame = menuFrame;
            // Load contacts asynchronously
            LoadContactsAsync();
        }

        private async void LoadContactsAsync()
        {
            try
            {
                var contacts = await GetContactsAsync();

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    Contacts.Clear(); // Clear existing contacts

                    foreach (var contact in contacts)
                    {
                        Contacts.Add(contact);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is Button button && button.CommandParameter is int contactId)
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5107");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserManager.Token.Token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                var response = await client.DeleteAsync($"users/contacts/{contactId}");

                if(!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Could not delete the contact");

                    return;
                }

                MessageBox.Show("Deleted Successfully");
                LoadContactsAsync();
            }
        }
        private void UpdateContact_Click( object sender, RoutedEventArgs e)
        {

        }

        public async Task<List<ContactDto>> GetContactsAsync()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5107");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserManager.Token.Token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            var response =  await client.GetFromJsonAsync<List<ContactDto>>($"users/{UserManager.Token.User.Id}/contacts");
           
            return response;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Content = new ContactAddPage(MenuFrame);
        }

    }
}
