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

namespace Jalkapallo_UI
{
    public partial class SellPlayer : PhoneApplicationPage
    {

        private bool isAsta = false;
        private int price = 0;
        private DateTime giornoAsta = DateTime.Now;
        int stipendioTeam = 0;
        private String[] SellTypeString = { "AuctionHouse", "Direct Sell" };

        public SellPlayer()
        {
            InitializeComponent();

            InitializeUI();

            EventsSubscription();
        }

        private void InitializeUI()
        {
            PlayerName.Text = App.currentPlayer.Name + " " + App.currentPlayer.Surname;

            foreach (Jalkapallo_Shared.Player p in App.currentTeam.TeamPlayers)
                stipendioTeam += p.Stipendio;

            datePicker.Value = DateTime.Now;
            this.datePicker.ValueChanged += new EventHandler<DateTimeValueChangedEventArgs>(picker_ValueChanged);
            datePicker.Visibility = System.Windows.Visibility.Collapsed;

            PriceValue.Text = price.ToString();
            SellType.ItemsSource = SellTypeString;
            SellType.SelectedItem = SellTypeString[1];
        }

        private void EventsSubscription()
        {
            App.ServiceClient.SellPlayerAuctionHouseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ServiceClient_SellPlayerAuctionHouseCompleted);
            App.ServiceClient.SellPlayerDirectlyCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ServiceClient_SellPlayerDirectlyCompleted);
        }

        void ServiceClient_SellPlayerDirectlyCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBoxResult m = MessageBox.Show("Now "+App.currentPlayer.Name+ " "+ App.currentPlayer.Surname+" is directly on sell", "INFO", MessageBoxButton.OK);
            if (m == MessageBoxResult.OK)
                return;
        }

        void ServiceClient_SellPlayerAuctionHouseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBoxResult m = MessageBox.Show("Now " + App.currentPlayer.Name + " " + App.currentPlayer.Surname + " is on auction house and it will expire at "+giornoAsta.Date.ToShortDateString(), "INFO", MessageBoxButton.OK);
            if (m == MessageBoxResult.OK)
                return;
        }

        private void ChangeSellType(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string value = (string)SellType.SelectedItem;

            switch (value)
            {
                case "AuctionHouse":
                    PriceLabel.Text = "Set Price:";
                    datePicker.Visibility = Visibility.Visible;
                    isAsta = true;
                    break;
                case "Direct Sell":
                    PriceLabel.Text = "Base Price:";
                    datePicker.Visibility = Visibility.Collapsed;
                    isAsta = false;
                    break;
            }
        }

        private void SetPrice(object sender, System.Windows.Input.KeyEventArgs e)
        {
			if (string.IsNullOrEmpty(PriceValue.Text) == false)
			{
				if (PriceValue.Text.Count() >= App.MAX_DIGITS_LENGTH)
				{
					MessageBoxResult mex = MessageBox.Show("Entered number exceeds the maximum allowed!", "ERROR", MessageBoxButton.OK);
					if (mex == MessageBoxResult.OK)
						return;
				}
				
				price = Convert.ToInt32(PriceValue.Text);				
			}
                
        }


		private void ClearPrice(object sender, System.Windows.Input.GestureEventArgs e)
        {
            price = 0;
            PriceValue.Text = price.ToString();
        }

        void picker_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            giornoAsta = (DateTime)e.NewDateTime;
        }

        private void Sell(object sender, System.Windows.RoutedEventArgs e)
        {
			if (price <= 0 || price > App.currentTeam.Budget + stipendioTeam)
            {
                MessageBoxResult mx = MessageBox.Show("Price value not valid", "Error", MessageBoxButton.OKCancel);
                if (mx == MessageBoxResult.OK || mx == MessageBoxResult.Cancel)
                {
                    return;
                }
            }

            MessageBoxResult m = MessageBox.Show("Are you sure to sell " + App.currentPlayer.Name + " " + App.currentPlayer.Surname + "?", "Sell confirm", MessageBoxButton.OKCancel);
            if (m == MessageBoxResult.OK)
            {
                // chiamo i metodi per vendere il giocatore!
                if (isAsta)
                    App.ServiceClient.SellPlayerAuctionHouseAsync(App.currentPlayer, price, giornoAsta);
                else
                    App.ServiceClient.SellPlayerDirectlyAsync(App.currentPlayer, price);
            }

            if (m == MessageBoxResult.Cancel)
                return;
        }


    }
}