using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using Jalkapallo_Shared;

using MatchClient = Jalkapallo_Shared.Match;
using PlayerClient = Jalkapallo_Shared.Player;

namespace JalkapalloWebRole
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
	[ServiceContract]
	public interface IWebRoleService
	{
		[OperationContract]
		int Registration(string username, string password, string email, string teamName);

		[OperationContract]
		string LoginUser(string userName, string password);

		[OperationContract]
		void SaveCurrentTeam(List<Player> currentFormation, string teamName);

		#region Get Methods

		[OperationContract]
		string GetTeam(string teamName);

		[OperationContract]
		string GetSavedTeam(string teamName);

		[OperationContract]
		string GetPlayersOnSellDirect();

		[OperationContract]
		string GetPlayersOnSellAuctionHouse();

		#endregion

		#region Search Engine

		[OperationContract]
		string GetAllTeamsAndPlayers();

		[OperationContract]
		string SearchTeams(string text);

		[OperationContract]
		string SearchPlayers(string text);

		#endregion

		#region Selling Engine

		[OperationContract]
		void SellPlayerDirectly(PlayerClient player, int price);   // TASK:0006 ++ 20130412 DB

		[OperationContract]
		void SellPlayerAuctionHouse(PlayerClient player, int startingPrice, DateTime endOfAuctionHouse);

		[OperationContract]
		void BuyPlayerDirectly(PlayerClient player, int price, string teamName);

		[OperationContract]
		bool MakeAnOfferForAPlayer(PlayerClient player, int offer, string teamNameOfBuyer);

		#endregion

		#region Training Engine

		[OperationContract]
		void SetTrainingType(string trainingType, string teamName);

		#endregion

		#region Push Notification

		[OperationContract]
		void SetPushNotificationChannel(string phoneId, string uri);

		#endregion
	}
}
