using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using MatchClient = Jalkapallo_Shared.Match;

namespace JalkapalloWorkerRoleWCF
{
	[ServiceContract]
	public interface IWorkerRoleService
	{
		[OperationContract]
		void ComputeTrainings();

		[OperationContract]
		void ComputeDirectlyPurchase();

		[OperationContract]
		void ComputeAuctionHouse();

		[OperationContract]
		void ComputeAllBudgets();

		[OperationContract]
		void CreateChampionship();

		// TEST
		[OperationContract]
		void CreateMatch();

		// TEST
		[OperationContract]
		MatchClient ComputeMatch(MatchClient currentMatch);
	}
}