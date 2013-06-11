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
using System.Globalization;

namespace Jalkapallo_UI
{
    public partial class DirectSell : PhoneApplicationPage
    {	
        public DirectSell()
        {
            InitializeComponent();
			EventSubscription();

            Player currentPlayer = App.currentPlayerDirectSell;
			Price.Text = App.currentPlayerPriceDirectSell.ToString();

            double temp;

            Name.Text = currentPlayer.Name + " " + currentPlayer.Surname;
            Country.Text = currentPlayer.Country;
            Role.Text = currentPlayer.Role.ToString();

           	string birthday = currentPlayer.Birthday.Replace(" 00:00:00", "");
            //DateTime birthday = DateTime.Parse(tempDate, CultureInfo.CurrentCulture);
			BirthDay.Text = "Birthday: " + birthday;
            Weight.Text = "Weight " + currentPlayer.Weight.ToString() + " Kg";
            Height.Text = "Height " + currentPlayer.Height.ToString() + " cm";
            Stipendio.Text = currentPlayer.Stipendio.ToString() + " €/week";

            EXP.Text = ((int)currentPlayer.Skills.Exp).ToString();
            temp = currentPlayer.Skills.Exp - Convert.ToDouble(EXP.Text);
            RECTexp.Width = 150 * temp;

            Speed.Text = ((int)currentPlayer.Skills.Velocita).ToString();
            temp = currentPlayer.Skills.Velocita - Convert.ToDouble(Speed.Text);
            RECTspeed.Width = 150 * temp;

            GoalKeeping.Text = ((int)currentPlayer.Skills.Parata).ToString();
            temp = currentPlayer.Skills.Parata - Convert.ToDouble(GoalKeeping.Text);
            RECTgk.Width = 150 * temp;

            Defense.Text = ((int)currentPlayer.Skills.Difesa).ToString();
            temp = currentPlayer.Skills.Difesa - Convert.ToDouble(Defense.Text);
            RECTdefense.Width = 150 * temp;

            Midfield.Text = ((int)currentPlayer.Skills.Centrocampo).ToString();
            temp = currentPlayer.Skills.Centrocampo - Convert.ToDouble(Midfield.Text);
            RECTmid.Width = 150 * temp;

            Attack.Text = ((int)currentPlayer.Skills.Attacco).ToString();
            temp = currentPlayer.Skills.Attacco - Convert.ToDouble(Attack.Text);
            RECTattack.Width = 150 * temp;

            Kick.Text = ((int)currentPlayer.Skills.Tiro).ToString();
            temp = currentPlayer.Skills.Tiro - Convert.ToDouble(Kick.Text);
            RECTkick.Width = 150 * temp;
        }

		private void EventSubscription()
		{
			App.ServiceClient.BuyPlayerDirectlyCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ServiceClient_BuyPlayerDirectlyCompleted);
		}

		void ServiceClient_BuyPlayerDirectlyCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{

		}

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult m = MessageBox.Show("Are you sure to buy " +App.currentPlayerDirectSell.Name+" "+ App.currentPlayerDirectSell.Surname+" ?", "INFO", MessageBoxButton.OKCancel);
			if (m == MessageBoxResult.OK)
				App.ServiceClient.BuyPlayerDirectlyAsync(App.currentPlayerDirectSell, App.currentPlayerPriceDirectSell, App.currentTeam.Name);
			else
				return;
        }
    }
}