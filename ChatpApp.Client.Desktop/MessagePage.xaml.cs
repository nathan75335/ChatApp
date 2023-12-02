using ChatApp.Shared.DTO_s;
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

namespace ChatApp.Client.Desktop
{
    /// <summary>
    /// Interaction logic for MessagePage.xaml
    /// </summary>
    public partial class MessagePage : Page
    {
        public MessagePage()
        {
            InitializeComponent();
            List<MessageDto> messages = GetMessages(); // Replace this with your message retrieval logic

            foreach (var message in messages)
            {
                AddMessageToChat(message);
            }
        }

        private List<MessageDto> GetMessages()
        {
            // Sample messages
            return new List<MessageDto>
            {
                new MessageDto { Id = 1, Sender = new UserDto { Name = "User A" }, Text = "Hello", TimeStamp = DateTime.Now },
                new MessageDto { Id = 2, Sender = new UserDto { Name = "User B" }, Text = "Hi there!", TimeStamp = DateTime.Now }
                // Add more messages as needed
            };
        }

        // Method to add a message to the chat panel
        private void AddMessageToChat(MessageDto message)
        {
            // Load message template from XAML resources
            var messageTemplate = Resources["MessageTemplate"] as StackPanel;

            if (messageTemplate != null)
            {
                // Create a new instance of the message template and set its DataContext to the message
                var newMessage = new StackPanel
                {
                    Style = messageTemplate.Style,
                    DataContext = message
                };

                // Add the new message to the chat panel
                //chatPanel.Children.Add(newMessage);
            }
        }
    }
}
