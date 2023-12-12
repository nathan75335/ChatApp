using ChatApp.Client.Desktop.Models;
using ChatApp.Shared.DTO_s;
using ChatApp.Shared.Requests.Message;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Compression;
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
    /// Interaction logic for MessagePage.xaml
    /// </summary>
    public partial class MessagePage : Page
    {
        public ObservableCollection<MessageViewModel> Messages { get; set; }
        
        public MessagePage()
        {
            Messages = new ObservableCollection<MessageViewModel>();
            InitializeComponent();
            DataContext = this;
            Loaded += Page_Loaded;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await StartLongPollingAndUpdateUIAsync(UserManager.Token.User.Id);
        }

        private async Task<IEnumerable<MessageDto>> LongPollForMessagesAsync(int userId)
        {
            string apiUrl = $"http://localhost:5107/users/{userId}/messages/new";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserManager.Token.Token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<MessageDto> newMessages = await response.Content.ReadFromJsonAsync<IEnumerable<MessageDto>>();
                        return newMessages;
                    }

                    MessageBox.Show("Could not load messages");

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }

        private async Task StartLongPollingAndUpdateUIAsync(int userId)
        {
            while (true)
            {
                var newMessages = await LongPollForMessagesAsync(userId);

                if (newMessages != null)
                {
                    var groupedMessages = newMessages.GroupBy(message => message.Sender.Id);
                    
                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                       List<MessageViewModel> list = new List<MessageViewModel>();
                        
                        foreach (var group in groupedMessages)
                        {
                            var latest = group.OrderByDescending(x => x.TimeStamp).First();
                            var messageViewModel = new MessageViewModel
                            {
                                Id = latest.Id,
                                ReceiverId = latest.Receiver.Id,
                                UserId = group.Key,
                                Title = latest.Sender.Name, 
                                Color = "#000000", // Set color as needed
                                IsRead = group.All(msg => msg.IsRead),
                                Message = latest.Text, 
                                MessageCount = group.Count()
                            };

                            list.Add(messageViewModel);
                        }

                        Messages.Clear();
                        foreach(var message in list)
                        {
                            Messages.Add(message);
                        }
                    });
                }

                // Wait for some time before making the next request
                await Task.Delay(2000);
            }
        }

        private async void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem listBoxItem && listBoxItem.DataContext is MessageViewModel message)
            {
                using var client  = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5107");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserManager.Token.Token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                var content = UserManager.GetContent(new MessageRequest
                {
                    Text = message.Message,
                    IsRead = "true"
                });
                var response = await client.PutAsync($"users/messages/{message.Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    UserMessage.Content = new UserMessagePage(message.UserId);
                }
               
            }
        }
    }
}
