using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Globalization;

using Jalkapallo_Shared;
using WebRoleWCFConnector.WebRoleServiceReference;

namespace Jalkapallo_UI
{
    public partial class Login : PhoneApplicationPage
    {
        public Login()
        {
            InitializeComponent();
			EventSubscriber();
        }

		private void EventSubscriber()
		{
			App.ServiceClient.LoginUserCompleted += new EventHandler<LoginUserCompletedEventArgs>(serviceClient_LoginUserCompleted);
		}

        private void OK_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (userName.Text.Length >= 13)
            {
                MessageBox.Show("Username too long, max 12 chars");
                userName.Text = "";
                password.Password = "";
                return;
            }

            //controllo su , e ;
            if (userName.Text.Contains(',') || userName.Text.Contains(';'))
            {
                MessageBox.Show("No comma and semicolon allowed!!");
                userName.Text = "";
                password.Password = "";
                return;
            }

            if (string.IsNullOrEmpty(userName.Text))
            {
                MessageBox.Show("Username is empty!!");
                userName.Text = "";
                password.Password = "";
                return;
            }

            if (password.Password == "")
            {
                MessageBox.Show("Password is empty!");
                userName.Text = "";
                password.Password = "";
                return;
            }

            ProgBar.Visibility = Visibility.Visible;

			App.ServiceClient.LoginUserAsync(userName.Text, password.Password);
        }

        private void serviceClient_LoginUserCompleted(object sender, LoginUserCompletedEventArgs e)
        {
            if (e.Error == null && e.Result != string.Empty)
            {
                MessageBoxResult result = MessageBox.Show("Welcome in Jalkapallo!", "Login Successful", MessageBoxButton.OK);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        ProgBar.Visibility = Visibility.Collapsed;

						// settaggio dati utente
						App.UserName = userName.Text;
						App.TeamName = e.Result;

                        NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                        break;
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Wrong username or password, please try again", "Login Unsuccessful", MessageBoxButton.OK);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        userName.Text = "";
                        password.Password = "";
                        break;
                }
            }

        }

        private void Register_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Registration.xaml", UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
			if (NavigationService.CanGoBack)
			{
				while (NavigationService.RemoveBackEntry() != null)
					NavigationService.RemoveBackEntry();
			}
        }
    }
}