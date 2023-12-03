﻿using ChatApp.Client.Desktop.Models;
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
            string apiUrl = $"http://localhost:5107/users/{userId}/messages/recent-conversation";

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
                    // Update UI with new messages using Dispatcher
                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                       List<MessageViewModel> list = new List<MessageViewModel>();
                        // Assuming you have a collection bound to your UI (e.g., ObservableCollection<MessageDto>)
                        foreach (MessageDto message in newMessages)
                        {
                            var messageViewModel = new MessageViewModel
                            {
                                Id = message.Id,
                                UserId = message.Sender.Id,
                                Title = message.Sender.Name,
                                Color = "#000000",
                                IsRead = message.IsRead,
                                Message = message.Text,
                                MessageCount = 1
                            };

                           
                            if (list.FirstOrDefault(x => x.UserId.Equals(messageViewModel.UserId) && x.Message.Equals(messageViewModel.Message)) is null)
                            {
                                list.Add(messageViewModel);
                            }
                            else
                            {
                                var model = list.FirstOrDefault(x => x.UserId == message.Sender.Id);
                                model.Message = message.Text;
                                model.MessageCount += 1;
                            }
                        }

                        Messages.Clear();
                        foreach(var message in list)
                        {
                            Messages.Add(message);
                        }
                    });
                }

                // Wait for some time before making the next request (e.g., 2 seconds)
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
