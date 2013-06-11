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
using System.IO.IsolatedStorage;
using Microsoft.Phone.Scheduler;
using System.Diagnostics;
using Microsoft.Phone.Notification;

using Jalkapallo_Shared;
using Jalkapallo_UI.DrawableObj;
using WebRoleWCFConnector.WebRoleServiceReference;

using PlayerClient = Jalkapallo_Shared.Player;
using TeamClient = Jalkapallo_Shared.Team;
using Microsoft.Phone.Info;

namespace Jalkapallo_UI
{
    public partial class MainPage : PhoneApplicationPage
    {
		private String[] TrainingListType = { "Goalkeeping", "Defense", "Midfield", "Attack", "Speed", "Shooting" };
		private Dictionary<string, List<string>> teamsAndPlayers = null;
		private Dictionary<PlayerClient, int> allOnSellDirect = null;
		private Dictionary<PlayerClient, AuctionHouse> allOnAuction = null;
        
        public MainPage()
        {
            InitializeComponent();
			EventsSubscription();

			if(App.currentTeam == null)
				QueryGetTeam();

			RetrieveTeamsAndPlayers();

            //Init Training Item
			TrainingType.ItemsSource = TrainingListType;           
        }

		private void EventsSubscription()
		{
			App.ServiceClient.GetTeamCompleted += new EventHandler<GetTeamCompletedEventArgs>(ServiceClient_GetTeamCompleted);
            App.ServiceClient.SetTrainingTypeCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ServiceClient_SetTrainingTypeCompleted);
			App.ServiceClient.GetAllTeamsAndPlayersCompleted += new EventHandler<GetAllTeamsAndPlayersCompletedEventArgs>(ServiceClient_GetAllTeamsAndPlayersCompleted);
			App.ServiceClient.GetPlayersOnSellDirectCompleted += new EventHandler<GetPlayersOnSellDirectCompletedEventArgs>(ServiceClient_GetPlayersOnSellDirectCompleted);
			App.ServiceClient.GetPlayersOnSellAuctionHouseCompleted += new EventHandler<GetPlayersOnSellAuctionHouseCompletedEventArgs>(ServiceClient_GetPlayersOnSellAuctionHouseCompleted);
		}

		void ServiceClient_GetPlayersOnSellAuctionHouseCompleted(object sender, GetPlayersOnSellAuctionHouseCompletedEventArgs e)
		{
			Asta.Items.Clear();
			allOnAuction = Deserializer.DeserializeObject<Dictionary<PlayerClient, AuctionHouse>>(e.Result);
			if (allOnAuction == null)
				return;

			foreach (KeyValuePair<PlayerClient, AuctionHouse> p in allOnAuction)
			{
				PlayerListEntry player = new PlayerListEntry();
				player.AssociatedPlayer = p.Key;
				player.PlayerName.Text = p.Key.Name + " " + p.Key.Surname;
				player.Role.Text = TranslateRole(p.Key.Role);
				Asta.Items.Add(player);
			}
		}

		void ServiceClient_GetPlayersOnSellDirectCompleted(object sender, GetPlayersOnSellDirectCompletedEventArgs e)
		{
			VenditaDiretta.Items.Clear();
			allOnSellDirect = Deserializer.DeserializeObject<Dictionary<PlayerClient, int>>(e.Result);
			if (allOnSellDirect == null)
				return;

			foreach (KeyValuePair<PlayerClient, int> p in allOnSellDirect)
			{
				PlayerListEntry player = new PlayerListEntry();
				player.AssociatedPlayer = p.Key;
				player.PlayerName.Text = p.Key.Name + " " + p.Key.Surname;
				player.Role.Text = TranslateRole(p.Key.Role);
				VenditaDiretta.Items.Add(player);
			}

		}		

        private void QueryGetTeam()
        {
			App.ServiceClient.GetTeamAsync(App.TeamName);
        }
        	
        #region PlayerItem

        private void PlayerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlayerListEntry selectedPlayer = (PlayerListEntry)this.PlayerList.SelectedItem;

            if (selectedPlayer != null)
            {
                App.currentPlayer = selectedPlayer.AssociatedPlayer;
                NavigationService.Navigate(new Uri("/PlayerPage.xaml", UriKind.Relative));
            }
        } 

        #endregion

        #region TrainingItem

        private void SetTraining(object sender, System.Windows.RoutedEventArgs e)
        {
            string trainingValue = TrainingType.SelectedItem.ToString();
            App.ServiceClient.SetTrainingTypeAsync(trainingValue, App.currentTeam.Name);
        }

        private void ServiceClient_SetTrainingTypeCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBoxResult m = MessageBox.Show("Training type updated!", "INFO", MessageBoxButton.OK);
            if (m == MessageBoxResult.OK)
                return;
        }

        #endregion

        #region News Item

        private void PurchaseListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }

        private void OtherNewsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }

        #endregion
        
        #region SearchItem

		private void RetrieveTeamsAndPlayers()
		{
			try
			{
				App.ServiceClient.GetAllTeamsAndPlayersAsync();
			}
			catch { }
		}

		

        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchBox.Text) == false)
            {
				if (teamsAndPlayers != null)
				{
					PlayerListBox.Items.Clear();
					TeamListBox.Items.Clear();

					foreach (string team in SearchTeams(SearchBox.Text))
						TeamListBox.Items.Add(team);

					foreach (string player in SearchPlayers(SearchBox.Text))
						PlayerListBox.Items.Add(player);
				}
            }
            else
            {
                TeamListBox.Items.Clear();
                PlayerListBox.Items.Clear();
            }
        }

		private List<string> SearchTeams(string text)
		{
			Debug.Assert(teamsAndPlayers != null);
			List<string> teams = new List<string>();

			foreach (string squadra in teamsAndPlayers.Keys)
			{
				if (squadra.ToLowerInvariant().Contains(text.ToLowerInvariant()) == false)
					continue;

				teams.Add(squadra);
			}

			return teams;
		}

		private List<string> SearchPlayers(string text)
		{
			Debug.Assert(teamsAndPlayers != null);
			List<string> players = new List<string>();

			foreach (List<string> teamPlayers in teamsAndPlayers.Values)
				foreach (string pl in teamPlayers)
				{
					if (pl.ToLowerInvariant().Contains(text.ToLowerInvariant()) == false)
						continue;

					players.Add(pl);
				}

			return players;
		}

        #endregion

        #region MatchItem

        private void SetFormation(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Formation.xaml", UriKind.Relative));
        }

        private void MatchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MatchListEntry selectedMatch = (MatchListEntry)this.MatchList.SelectedItem;
        }

        #endregion              

		#region Service Client Events

		private void ServiceClient_GetTeamCompleted(object sender, GetTeamCompletedEventArgs e)
		{
			if (e.Result != null)
			{
				App.currentTeam = Deserializer.DeserializeObject<TeamClient>(e.Result);

				//visualizza elenco dei giocatori nella schermata PLAYERS 
				foreach (PlayerClient p in App.currentTeam.TeamPlayers)
				{
					PlayerListEntry player = new PlayerListEntry();
					player.AssociatedPlayer = p;
					player.PlayerName.Text = p.Name + " " + p.Surname;
					player.Role.Text = TranslateRole(p.Role);
					PlayerList.Items.Add(player);
				}

				// Setting Coach Tab
				LabelCoachName.Text = App.currentTeam.Coach.CoachName.ToString();
				LabelCoachSurname.Text = App.currentTeam.Coach.CoachSurname.ToString();
				LabelCoachAbilityValue.Text = App.currentTeam.Coach.CoachAbility.ToString();
				TrainingType.SelectedItem = App.currentTeam.Coach.TrainingType;

                //Setting Budget Tab -- Mancano i dettagli di IN/OUT 
                BudgetValue.Text = App.currentTeam.Budget.ToString();

				//visualizza elenco dei match nella schermata MATCHES 
				//foreach (Match m in App.currentTeam.Matches)
				//{
				//    MatchListEntry match = new MatchListEntry();
				//    match.AssociatedMatch = m;
				//    match.Squadre.Text = m.Home.Name + "-" + m.Away.Name;
				//    match.Risultato.Text = m.ResultHome + "-" + m.ResultAway;
				//    MatchList.Items.Add(match);
				//}
			}
		}

        private string TranslateRole(RolePlayerEnum rolePlayerEnum)
        {
            string role = string.Empty;
            switch (rolePlayerEnum)
            {
                case RolePlayerEnum.Allenatore:
                    role= "Coach";
                    break;
                case RolePlayerEnum.Attaccante:
                    role= "Striker";
                    break;
                case RolePlayerEnum.CentroCampista:
                    role = "Midfielder";
                    break;
                case RolePlayerEnum.Difensore:
                    role = "Defender";
                    break;
                case RolePlayerEnum.Portiere:
                    role = "GoalKeeper";
                    break;
            }
            return role;
        }

		private void ServiceClient_GetAllTeamsAndPlayersCompleted(object sender, GetAllTeamsAndPlayersCompletedEventArgs e)
		{
			if (string.IsNullOrEmpty(e.Result) == true)
				return;

			// salvo nell'IsolatedStorage ("jalkapallo_SearchInfo.xml") tutte le squadre e giocatori
			SearchEngineStore.Store<Dictionary<string, List<string>>>(Deserializer.DeserializeObject<Dictionary<string, List<string>>>(e.Result));
		}

		#endregion       

        #region Eventi Trasferimenti

        private void VenditaDiretta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlayerListEntry selectedPlayer = (PlayerListEntry)this.VenditaDiretta.SelectedItem;
			if (selectedPlayer == null)
				return;

			int price = allOnSellDirect.Select(x => x).Where(k => k.Key.Guid == selectedPlayer.AssociatedPlayer.Guid).SingleOrDefault().Value;

            if (selectedPlayer != null)
            {
                App.currentPlayerDirectSell = selectedPlayer.AssociatedPlayer;
				App.currentPlayerPriceDirectSell = price;
                NavigationService.Navigate(new Uri("/DirectSell.xaml", UriKind.Relative));
            }
        }

        private void Asta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlayerListEntry selectedPlayer = (PlayerListEntry)this.Asta.SelectedItem;
			if (selectedPlayer == null)
				return;

			AuctionHouse ah = allOnAuction.Select(x => x).Where(k => k.Key.Guid == selectedPlayer.AssociatedPlayer.Guid).SingleOrDefault().Value;

            if (selectedPlayer != null)
            {
                App.currentPlayerAsta = selectedPlayer.AssociatedPlayer;
				App.currentAuctionHouseObj = ah;
                NavigationService.Navigate(new Uri("/AstaPage.xaml", UriKind.Relative));
            }
        } 

        #endregion

        #region Push Notification

        #endregion

        #region LOADING PIVOT ITEM
        private void Pivot_LoadingPivotItem(object sender, Microsoft.Phone.Controls.PivotItemEventArgs e)
        {
            if (e.Item == this.Search)
            {
                teamsAndPlayers = SearchEngineStore.Retrieve<Dictionary<string, List<string>>>();
            }
            if (e.Item == this.Transfer)
            {
                //aggiungo giocatori asta
				App.ServiceClient.GetPlayersOnSellAuctionHouseAsync();

                //aggiungo giocatori directSell 
				App.ServiceClient.GetPlayersOnSellDirectAsync();
            }
        } 
        #endregion

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult mb = MessageBox.Show("You want exit the page", "Alert", MessageBoxButton.OKCancel);

            if (mb == MessageBoxResult.Cancel)
                e.Cancel = true;

            if (mb == MessageBoxResult.OK)
            {
                if (NavigationService.CanGoBack)
                    while (NavigationService.RemoveBackEntry() != null)
                        NavigationService.RemoveBackEntry();
            }
        }
       
    }
}