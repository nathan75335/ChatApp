using ChatApp.Shared.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
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
using System.Net.Http.Json;

namespace ChatApp.Client.Desktop
{
    /// <summary>
    /// Interaction logic for UserMessagePage.xaml
    /// </summary>
    public partial class UserMessagePage : Page
    {
        public int SenderId {  get; set; }
        public UserMessagePage(int senderId)
        {
            this.SenderId = senderId;
            InitializeComponent();
            Loaded += Page_Loaded; 
        }

        private async Task<IEnumerable<MessageDto>> LoadMessagesAsync(int userId)
        {
            string apiUrl = $"http://localhost:5107/users/{SenderId}/messages/{userId}";

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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                var messages = await LoadMessagesAsync(UserManager.Token.User.Id);
                var listOrdered = messages.OrderBy(x => x.TimeStamp);
                userMessagesPanel.Children.Clear();

                foreach (var message in listOrdered)
                {
                    var border = new Border();
                    var textBlock = new TextBlock();
                    
                    if (message.Sender.Id == UserManager.Token.User.Id)
                    {
                        border.CornerRadius = new CornerRadius(10, 0, 10, 10);
                        border.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF8, 0xF7, 0xFC));
                        border.Padding = new Thickness(10, 7, 10, 7);
                        border.HorizontalAlignment = HorizontalAlignment.Right;
                        border.Margin = new Thickness(0, 5, 0, 0);
                        textBlock.FontSize = 14;
                        textBlock.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x94, 0x93, 0x9A));
                        textBlock.VerticalAlignment = VerticalAlignment.Center;
                        textBlock.Margin = new Thickness(3, 0, 3, 2);
                    }
                    else
                    {
                        border.CornerRadius = new CornerRadius(10, 0, 10, 10);
                        border.Background = new SolidColorBrush(Color.FromRgb(248, 247, 252));
                        border.Padding = new Thickness(10, 7, 10, 7);
                        border.HorizontalAlignment = HorizontalAlignment.Left;
                        border.Margin = new Thickness(0, 5, 0, 0);
                        textBlock.FontSize = 14;
                        textBlock.Foreground = new SolidColorBrush(Color.FromRgb(148, 147, 154));
                        textBlock.VerticalAlignment = VerticalAlignment.Center;
                        textBlock.Margin = new Thickness(3, 0, 3, 2);
                    }


                    textBlock.Text = $"{message.Sender.Username}: {message.Text}";
                    border.Child = textBlock;
                    userMessagesPanel.Children.Add(border);
                }

                await Task.Delay(2000);
            }
        }
    }
}
