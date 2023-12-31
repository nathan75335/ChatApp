﻿using ChatApp.Client.Desktop;
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            MenuFrame.Content = new ContactPage(MenuFrame);
        }

        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Content = new ContactPage(MenuFrame);
        }

        private void MessageButton_Click(object sender, RoutedEventArgs e)
        {
            if(UserManager.Token.User.Role.Name == "Admin")
            {
                MenuFrame.Content = new AdminPage();
            }
            else
            {
                MenuFrame.Content = new MessagePage();
            }
            
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
