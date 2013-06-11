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

using Jalkapallo_UI;
using WebRoleWCFConnector.WebRoleServiceReference;

namespace Jalkapallo_UI
{
    public partial class Registration : PhoneApplicationPage
    {
        private string username, passw, mail, team;

        public Registration()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ProgBar.Visibility = Visibility.Visible;

            //controllo se ho riempito tutte le caselle di testo
            if (string.IsNullOrEmpty(nickName.Text) ||
                password1.Password == "" || 
                password2.Password == "" ||
                string.IsNullOrEmpty(email.Text) ||
                string.IsNullOrEmpty(teamName.Text))
            {
                MessageBox.Show("Please fill all the boxes");
                ClearBoxes();
                return;
            }

            //ottengo le stringhe dalle box e gestisco subito errore in caso di password diverse 
            username = nickName.Text;
            mail = email.Text;
            team = teamName.Text;

            if (string.Equals(password1.Password, password2.Password))
			{
				passw = password1.Password;
			}                
            else
            {
                MessageBoxResult resultPassw = MessageBox.Show("Passwords do not match", "Server Message", MessageBoxButton.OK);
                switch (resultPassw)
                {
                    case MessageBoxResult.OK:
                        ClearBoxes();
                        return;
                }
            }

			// se tutto è corretto procediamo con la registrazione!
			// validazione è lato server
			App.ServiceClient.RegistrationCompleted += new EventHandler<RegistrationCompletedEventArgs>(serviceClient_RegistrationCompleted);
			App.ServiceClient.RegistrationAsync(username, passw, mail, team);           
        }

		private void serviceClient_RegistrationCompleted(object sender, RegistrationCompletedEventArgs e)
		{
			// come gestiamo le eccezioni ??
			if (e.Error != null)
			{
				throw new Exception();
			}

			switch (e.Result)
			{
				//se =0 ok
				case 0:
					MessageBoxResult result0 = MessageBox.Show("Registration Successfully!", "Server Message", MessageBoxButton.OK);
					switch (result0)
					{
						case MessageBoxResult.OK:
                            ProgBar.Visibility = Visibility.Collapsed;
							
							// settaggio dati utente
							App.UserName = username;
							App.TeamName = team;

							NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
							break;
					}
					break;

				//se =1 user già esistente
				case 1:
					MessageBoxResult result1 = MessageBox.Show("Username already exists", "Server Message", MessageBoxButton.OK);
					switch (result1)
					{
						case MessageBoxResult.OK:
							ClearBoxes();
							break;
					}
					break;

				//se =2 email già esistente
				case 2:
					MessageBoxResult result2 = MessageBox.Show("Email already exists", "Server Message", MessageBoxButton.OK);
					switch (result2)
					{
						case MessageBoxResult.OK:
							ClearBoxes();
							break;
					}
					break;
				//se =3 teamName già esistente
				case 3:
					MessageBoxResult result3 = MessageBox.Show("Team name already exists", "Server Message", MessageBoxButton.OK);
					switch (result3)
					{
						case MessageBoxResult.OK:
							ClearBoxes();
							break;
					}
					break;
			}
		}

        private void ClearBoxes()
        {
            ProgBar.Visibility = Visibility.Collapsed;
            nickName.Text = "";
            password1.Password = "";
            password2.Password = "";
            email.Text = "";
            teamName.Text = "";
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }
    }
}