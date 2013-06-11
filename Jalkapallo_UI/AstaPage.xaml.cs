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
	public partial class AstaPage : PhoneApplicationPage
	{
		private readonly Key[] numeric = new Key[] {Key.Back, Key.NumPad0, Key.NumPad1, 
            Key.NumPad2, Key.NumPad3, Key.NumPad4,Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9 };

		public AstaPage()
		{
			InitializeComponent();
			EventSubscription();

			Player currentPlayer = App.currentPlayerAsta;

			Price.Text = App.currentAuctionHouseObj.CurrentOffer.ToString();
			BestOfferer.Text = App.currentAuctionHouseObj.ActualTeam;

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
			App.ServiceClient.MakeAnOfferForAPlayerCompleted += new EventHandler<WebRoleWCFConnector.WebRoleServiceReference.MakeAnOfferForAPlayerCompletedEventArgs>(ServiceClient_MakeAnOfferForAPlayerCompleted);
		}

		void ServiceClient_MakeAnOfferForAPlayerCompleted(object sender, WebRoleWCFConnector.WebRoleServiceReference.MakeAnOfferForAPlayerCompletedEventArgs e)
		{
		}

		private void Offer_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult m = MessageBox.Show("Are you want to make an offer of " + MyOffer.Text + "€ for " + App.currentPlayerAsta.Name + " " + App.currentPlayerAsta.Surname + " ?", "INFO", MessageBoxButton.OKCancel);
			if (m == MessageBoxResult.OK)
			{
				if (MyOffer.Text.Count() <= App.MAX_DIGITS_LENGTH)
				{
					if (Int32.Parse(MyOffer.Text) > Int32.Parse(App.currentTeam.Budget.ToString()))
					{
						MessageBoxResult mex = MessageBox.Show("Budget too low!", "ERROR", MessageBoxButton.OK);
						if (mex == MessageBoxResult.OK)
							return;
					}
					else
					{
						App.ServiceClient.MakeAnOfferForAPlayerAsync(App.currentPlayerAsta, Convert.ToInt32(MyOffer.Text), App.currentTeam.Name);
					}
				}
				else
				{
					MessageBoxResult mex = MessageBox.Show("Entered number exceeds the maximum allowed!", "ERROR", MessageBoxButton.OK);
					if (mex == MessageBoxResult.OK)
						return;
				}
			}
			else
				return;
		}

		private void MyOffer_KeyDown(object sender, KeyEventArgs e)
		{
			// handles non numeric
			if (Array.IndexOf(numeric, e.Key) == -1)
			{
				e.Handled = true;
			}
		}
	}
}