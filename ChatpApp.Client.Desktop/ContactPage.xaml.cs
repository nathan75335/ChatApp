using ChatApp.Shared.DTO_s;
using ChatpApp.Client.Desktop;
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

        public ContactPage()
        {
            InitializeComponent();
            DataContext = this;

            // Initialize the ObservableCollection
            Contacts = new ObservableCollection<ContactDto>();

            // Load contacts asynchronously
            LoadContactsAsync();
        }

        private async void LoadContactsAsync()
        {
            try
            {
                var contacts = await GetContactsAsync();

                // Update UI on the main thread
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    Contacts.Clear(); // Clear existing contacts

                    foreach (var contact in contacts)
                    {
                        Contacts.Add(contact); // Add fetched contacts to the ObservableCollection
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            // Handle contact deletion
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
    }
}
