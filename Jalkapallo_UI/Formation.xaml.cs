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
using Jalkapallo_Shared;
using System.Windows.Data;
using System.Globalization;

namespace Jalkapallo_UI
{
    public partial class Formation : PhoneApplicationPage
    {
        List<Player> newFormation = new List<Player>();

        Player portiere = new Player();
        Player difensore1 = new Player();
        Player difensore2 = new Player();
        Player difensore3 = new Player();
        Player difensore4 = new Player();
        Player difensore5 = new Player();
        Player centrocampista1 = new Player();
        Player centrocampista2 = new Player();
        Player centrocampista3 = new Player();
        Player centrocampista4 = new Player();
        Player centrocampista5 = new Player();
        Player attaccante1 = new Player();
        Player attaccante2 = new Player();
        Player attaccante3 = new Player();

        public Formation()
        {
            InitializeComponent();

            EventsSubscription();

            List<Jalkapallo_Shared.PlayerSkills> temp = new List<PlayerSkills>();

            PT1.ItemsSource = App.currentTeam.TeamPlayers;
            DF1.ItemsSource = App.currentTeam.TeamPlayers;
            DF2.ItemsSource = App.currentTeam.TeamPlayers;
            DF3.ItemsSource = App.currentTeam.TeamPlayers;
            DF4.ItemsSource = App.currentTeam.TeamPlayers;
            DF5.ItemsSource = App.currentTeam.TeamPlayers;
            CC1.ItemsSource = App.currentTeam.TeamPlayers;
            CC2.ItemsSource = App.currentTeam.TeamPlayers;
            CC3.ItemsSource = App.currentTeam.TeamPlayers;
            CC4.ItemsSource = App.currentTeam.TeamPlayers;
            CC5.ItemsSource = App.currentTeam.TeamPlayers;
            AT1.ItemsSource = App.currentTeam.TeamPlayers;
            AT2.ItemsSource = App.currentTeam.TeamPlayers;
            AT3.ItemsSource = App.currentTeam.TeamPlayers;

            App.ServiceClient.GetSavedTeamAsync(App.currentTeam.Name);
        }


        private void EventsSubscription()
        {
            App.ServiceClient.SaveCurrentTeamCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ServiceClient_SaveCurrentTeamCompleted);
            App.ServiceClient.GetSavedTeamCompleted += new EventHandler<WebRoleWCFConnector.WebRoleServiceReference.GetSavedTeamCompletedEventArgs>(ServiceClient_GetSavedTeamCompleted);
        }


        #region Service Client Events
        void ServiceClient_GetSavedTeamCompleted(object sender, WebRoleWCFConnector.WebRoleServiceReference.GetSavedTeamCompletedEventArgs e)
        {
            int attaccanti = 0, centrocampisti = 0, difensori = 0;

            if (e.Result != null)
            {
                List<Player> tempTeam = Deserializer.DeserializeObject<List<Player>>(e.Result);

                for (int i = 0; i < 11; i++)
                {
                    switch (tempTeam[i].Role)
                    {
                        case RolePlayerEnum.Difensore:
                            difensori++;
                            break;
                        case RolePlayerEnum.CentroCampista:
                            centrocampisti++;
                            break;
                        case RolePlayerEnum.Attaccante:
                            attaccanti++;
                            break;
                    }
                }

                #region GET formation
                if (difensori == 4 && centrocampisti == 4 && attaccanti == 2)
                {
                    CurrentFormation.Text = "Current formation: 442";

                    ClearButtonVisibility();

                    PT1.Visibility = Visibility.Visible;

                    DF1.Visibility = Visibility.Visible;
                    DF2.Visibility = Visibility.Visible;
                    DF3.Visibility = Visibility.Visible;
                    DF4.Visibility = Visibility.Visible;

                    CC1.Visibility = Visibility.Visible;
                    CC2.Visibility = Visibility.Visible;
                    CC3.Visibility = Visibility.Visible;
                    CC4.Visibility = Visibility.Visible;

                    AT1.Visibility = Visibility.Visible;
                    AT3.Visibility = Visibility.Visible;

                    PT1.SelectedItem = App.currentTeam.TeamPlayers[0];

                    DF1.SelectedItem = App.currentTeam.TeamPlayers[1];
                    DF2.SelectedItem = App.currentTeam.TeamPlayers[2];
                    DF3.SelectedItem = App.currentTeam.TeamPlayers[3];
                    DF4.SelectedItem = App.currentTeam.TeamPlayers[4];

                    CC1.SelectedItem = App.currentTeam.TeamPlayers[5];
                    CC2.SelectedItem = App.currentTeam.TeamPlayers[6];
                    CC3.SelectedItem = App.currentTeam.TeamPlayers[7];
                    CC4.SelectedItem = App.currentTeam.TeamPlayers[8];

                    AT1.SelectedItem = App.currentTeam.TeamPlayers[9];
                    AT2.SelectedItem = App.currentTeam.TeamPlayers[10];

                }

                if (difensori == 3 && centrocampisti == 5 && attaccanti == 2)
                {
                    CurrentFormation.Text = "Current formation: 352";

                    ClearButtonVisibility();

                    PT1.Visibility = Visibility.Visible;

                    DF2.Visibility = Visibility.Visible;
                    DF3.Visibility = Visibility.Visible;
                    DF4.Visibility = Visibility.Visible;

                    CC1.Visibility = Visibility.Visible;
                    CC2.Visibility = Visibility.Visible;
                    CC3.Visibility = Visibility.Visible;
                    CC4.Visibility = Visibility.Visible;
                    CC5.Visibility = Visibility.Visible;

                    AT1.Visibility = Visibility.Visible;
                    AT3.Visibility = Visibility.Visible;

                    PT1.SelectedItem = App.currentTeam.TeamPlayers[0];

                    DF1.SelectedItem = App.currentTeam.TeamPlayers[1];
                    DF2.SelectedItem = App.currentTeam.TeamPlayers[2];
                    DF3.SelectedItem = App.currentTeam.TeamPlayers[3];

                    CC1.SelectedItem = App.currentTeam.TeamPlayers[4];
                    CC2.SelectedItem = App.currentTeam.TeamPlayers[5];
                    CC3.SelectedItem = App.currentTeam.TeamPlayers[6];
                    CC4.SelectedItem = App.currentTeam.TeamPlayers[7];
                    CC5.SelectedItem = App.currentTeam.TeamPlayers[8];

                    AT1.SelectedItem = App.currentTeam.TeamPlayers[9];
                    AT2.SelectedItem = App.currentTeam.TeamPlayers[10];
                }
                if (difensori == 4 && centrocampisti == 3 && attaccanti == 3)
                {
                    CurrentFormation.Text = "Current formation: 433";

                    ClearButtonVisibility();

                    PT1.Visibility = Visibility.Visible;

                    DF1.Visibility = Visibility.Visible;
                    DF2.Visibility = Visibility.Visible;
                    DF3.Visibility = Visibility.Visible;
                    DF4.Visibility = Visibility.Visible;

                    CC2.Visibility = Visibility.Visible;
                    CC3.Visibility = Visibility.Visible;
                    CC4.Visibility = Visibility.Visible;

                    AT1.Visibility = Visibility.Visible;
                    AT2.Visibility = Visibility.Visible;
                    AT3.Visibility = Visibility.Visible;

                    PT1.SelectedItem = App.currentTeam.TeamPlayers[0];

                    DF1.SelectedItem = App.currentTeam.TeamPlayers[1];
                    DF2.SelectedItem = App.currentTeam.TeamPlayers[2];
                    DF3.SelectedItem = App.currentTeam.TeamPlayers[3];
                    DF4.SelectedItem = App.currentTeam.TeamPlayers[4];

                    CC1.SelectedItem = App.currentTeam.TeamPlayers[5];
                    CC2.SelectedItem = App.currentTeam.TeamPlayers[6];
                    CC3.SelectedItem = App.currentTeam.TeamPlayers[7];

                    AT1.SelectedItem = App.currentTeam.TeamPlayers[8];
                    AT2.SelectedItem = App.currentTeam.TeamPlayers[9];
                    AT3.SelectedItem = App.currentTeam.TeamPlayers[10];
                }

                if (difensori == 3 && centrocampisti == 4 && attaccanti == 3)
                {
                    CurrentFormation.Text = "Current formation: 343";

                    ClearButtonVisibility();

                    PT1.Visibility = Visibility.Visible;

                    DF2.Visibility = Visibility.Visible;
                    DF3.Visibility = Visibility.Visible;
                    DF4.Visibility = Visibility.Visible;

                    CC1.Visibility = Visibility.Visible;
                    CC2.Visibility = Visibility.Visible;
                    CC3.Visibility = Visibility.Visible;
                    CC4.Visibility = Visibility.Visible;

                    AT1.Visibility = Visibility.Visible;
                    AT2.Visibility = Visibility.Visible;
                    AT3.Visibility = Visibility.Visible;

                    PT1.SelectedItem = App.currentTeam.TeamPlayers[0];

                    DF1.SelectedItem = App.currentTeam.TeamPlayers[1];
                    DF2.SelectedItem = App.currentTeam.TeamPlayers[2];
                    DF3.SelectedItem = App.currentTeam.TeamPlayers[3];

                    CC1.SelectedItem = App.currentTeam.TeamPlayers[4];
                    CC2.SelectedItem = App.currentTeam.TeamPlayers[5];
                    CC3.SelectedItem = App.currentTeam.TeamPlayers[6];
                    CC4.SelectedItem = App.currentTeam.TeamPlayers[7];

                    AT1.SelectedItem = App.currentTeam.TeamPlayers[8];
                    AT2.SelectedItem = App.currentTeam.TeamPlayers[9];
                    AT3.SelectedItem = App.currentTeam.TeamPlayers[10];
                }
                if (difensori == 5 && centrocampisti == 3 && attaccanti == 2)
                {
                    CurrentFormation.Text = "Current formation: 532";

                    ClearButtonVisibility();

                    PT1.Visibility = Visibility.Visible;

                    DF1.Visibility = Visibility.Visible;
                    DF2.Visibility = Visibility.Visible;
                    DF3.Visibility = Visibility.Visible;
                    DF4.Visibility = Visibility.Visible;
                    DF5.Visibility = Visibility.Visible;

                    CC2.Visibility = Visibility.Visible;
                    CC3.Visibility = Visibility.Visible;
                    CC4.Visibility = Visibility.Visible;

                    AT1.Visibility = Visibility.Visible;
                    AT3.Visibility = Visibility.Visible;

                    PT1.SelectedItem = App.currentTeam.TeamPlayers[0];

                    DF1.SelectedItem = App.currentTeam.TeamPlayers[1];
                    DF2.SelectedItem = App.currentTeam.TeamPlayers[2];
                    DF3.SelectedItem = App.currentTeam.TeamPlayers[3];
                    DF4.SelectedItem = App.currentTeam.TeamPlayers[4];
                    DF5.SelectedItem = App.currentTeam.TeamPlayers[5];

                    CC1.SelectedItem = App.currentTeam.TeamPlayers[6];
                    CC2.SelectedItem = App.currentTeam.TeamPlayers[7];
                    CC3.SelectedItem = App.currentTeam.TeamPlayers[8];

                    AT1.SelectedItem = App.currentTeam.TeamPlayers[9];
                    AT2.SelectedItem = App.currentTeam.TeamPlayers[10];
                }
                #endregion
            }
        }

        void ServiceClient_SaveCurrentTeamCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion


        private void ClearButtonVisibility()
        {
            PT1.Visibility = Visibility.Collapsed;

            DF1.Visibility = Visibility.Collapsed;
            DF2.Visibility = Visibility.Collapsed;
            DF3.Visibility = Visibility.Collapsed;
            DF4.Visibility = Visibility.Collapsed;
            DF5.Visibility = Visibility.Collapsed;

            CC1.Visibility = Visibility.Collapsed;
            CC2.Visibility = Visibility.Collapsed;
            CC3.Visibility = Visibility.Collapsed;
            CC4.Visibility = Visibility.Collapsed;
            CC5.Visibility = Visibility.Collapsed;

            AT1.Visibility = Visibility.Collapsed;
            AT2.Visibility = Visibility.Collapsed;
            AT3.Visibility = Visibility.Collapsed;
        }

        private void QQD(object sender, System.EventArgs e)
        {
            CurrentFormation.Text = "Current formation: 442";

            ClearButtonVisibility();

            PT1.Visibility = Visibility.Visible;

            DF1.Visibility = Visibility.Visible;
            DF2.Visibility = Visibility.Visible;
            DF3.Visibility = Visibility.Visible;
            DF4.Visibility = Visibility.Visible;

            CC1.Visibility = Visibility.Visible;
            CC2.Visibility = Visibility.Visible;
            CC3.Visibility = Visibility.Visible;
            CC4.Visibility = Visibility.Visible;

            AT1.Visibility = Visibility.Visible;
            AT3.Visibility = Visibility.Visible;

        }

        private void TCD(object sender, System.EventArgs e)
        {
            CurrentFormation.Text = "Current formation: 352";

            ClearButtonVisibility();

            PT1.Visibility = Visibility.Visible;

            DF2.Visibility = Visibility.Visible;
            DF3.Visibility = Visibility.Visible;
            DF4.Visibility = Visibility.Visible;

            CC1.Visibility = Visibility.Visible;
            CC2.Visibility = Visibility.Visible;
            CC3.Visibility = Visibility.Visible;
            CC4.Visibility = Visibility.Visible;
            CC5.Visibility = Visibility.Visible;

            AT1.Visibility = Visibility.Visible;
            AT3.Visibility = Visibility.Visible;
        }

        private void QTT(object sender, System.EventArgs e)
        {
            CurrentFormation.Text = "Current formation: 433";

            ClearButtonVisibility();

            PT1.Visibility = Visibility.Visible;

            DF1.Visibility = Visibility.Visible;
            DF2.Visibility = Visibility.Visible;
            DF3.Visibility = Visibility.Visible;
            DF4.Visibility = Visibility.Visible;

            CC2.Visibility = Visibility.Visible;
            CC3.Visibility = Visibility.Visible;
            CC4.Visibility = Visibility.Visible;

            AT1.Visibility = Visibility.Visible;
            AT2.Visibility = Visibility.Visible;
            AT3.Visibility = Visibility.Visible;


        }

        private void TQT(object sender, System.EventArgs e)
        {
            CurrentFormation.Text = "Current formation: 343";

            ClearButtonVisibility();

            PT1.Visibility = Visibility.Visible;

            DF2.Visibility = Visibility.Visible;
            DF3.Visibility = Visibility.Visible;
            DF4.Visibility = Visibility.Visible;

            CC1.Visibility = Visibility.Visible;
            CC2.Visibility = Visibility.Visible;
            CC3.Visibility = Visibility.Visible;
            CC4.Visibility = Visibility.Visible;

            AT1.Visibility = Visibility.Visible;
            AT2.Visibility = Visibility.Visible;
            AT3.Visibility = Visibility.Visible;

        }

        private void CTD(object sender, System.EventArgs e)
        {
            CurrentFormation.Text = "Current formation: 532";

            ClearButtonVisibility();

            PT1.Visibility = Visibility.Visible;

            DF1.Visibility = Visibility.Visible;
            DF2.Visibility = Visibility.Visible;
            DF3.Visibility = Visibility.Visible;
            DF4.Visibility = Visibility.Visible;
            DF5.Visibility = Visibility.Visible;

            CC2.Visibility = Visibility.Visible;
            CC3.Visibility = Visibility.Visible;
            CC4.Visibility = Visibility.Visible;

            AT1.Visibility = Visibility.Visible;
            AT3.Visibility = Visibility.Visible;

        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult mb = MessageBox.Show("Current formation saved!", "Page Closing", MessageBoxButton.OK);
            {
                if (mb == MessageBoxResult.OK)
                {
                    SetRiserve();
                    //App.ServiceClient.SaveCurrentTeamAsync( newFormation,App.currentTeam.Name);
                }

            }
        }

        private void SetRiserve()
        {
            List<Player> temp = new List<Player>();

            foreach (Player p in App.currentTeam.TeamPlayers)
            {
                if (newFormation.Contains(p) == false)
                    temp.Add(p);
            }

            newFormation.AddRange(temp);
        }

        #region Event Players
        private void SelectPT(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Player selectedPlayer = (sender as ListPicker).SelectedItem as Player;
            portiere = selectedPlayer;
        }

        private void SelectDF1(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Player selectedPlayer = (sender as ListPicker).SelectedItem as Player;
            difensore1 = selectedPlayer;
        }

        private void SelectDF2(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Player selectedPlayer = (sender as ListPicker).SelectedItem as Player;
            difensore2 = selectedPlayer;
        }

        private void SelectDF3(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Player selectedPlayer = (sender as ListPicker).SelectedItem as Player;
            difensore3 = selectedPlayer;
        }

        private void SelectDF4(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Player selectedPlayer = (sender as ListPicker).SelectedItem as Player;
            difensore4 = selectedPlayer;
        }

        private void SelectDF5(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Player selectedPlayer = (sender as ListPicker).SelectedItem as Player;
            difensore5 = selectedPlayer;
        }

        private void SelectCC1(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Player selectedPlayer = (sender as ListPicker).SelectedItem as Player;
            centrocampista1 = selectedPlayer;
        }

        private void SelectCC2(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Player selectedPlayer = (sender as ListPicker).SelectedItem as Player;
            centrocampista2 = selectedPlayer;
        }

        private void SelectCC3(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Player selectedPlayer = (sender as ListPicker).SelectedItem as Player;
            centrocampista3 = selectedPlayer;
        }

        private void SelectCC4(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Player selectedPlayer = (sender as ListPicker).SelectedItem as Player;
            centrocampista4 = selectedPlayer;
        }

        private void SelectCC5(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Player selectedPlayer = (sender as ListPicker).SelectedItem as Player;
            centrocampista5 = selectedPlayer;
        }

        private void SelectAT1(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Player selectedPlayer = (sender as ListPicker).SelectedItem as Player;
            attaccante1 = selectedPlayer;
        }

        private void SelectAT2(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Player selectedPlayer = (sender as ListPicker).SelectedItem as Player;
            attaccante2 = selectedPlayer;
        }

        private void SelectAT3(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Player selectedPlayer = (sender as ListPicker).SelectedItem as Player;
            attaccante3 = selectedPlayer;
        }

        #endregion

    }

    public class SimpleTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {            
            return System.Convert.ToInt32(value); 
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

}