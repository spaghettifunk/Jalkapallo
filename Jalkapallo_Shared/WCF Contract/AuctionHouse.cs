using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Jalkapallo_Shared
{
	[DataContract]
	public class AuctionHouse
	{
		#region Properties

		[DataMember]
		public string OriginalTeam { get; set; }

		[DataMember]
		public string ActualTeam { get; set; }

		[DataMember]
		public int CurrentOffer { get; set; }

		[DataMember]
		public Guid PlayerId { get; set; }

		[DataMember]
		public string PlayerName { get; set; }

		[DataMember]
		public string PlayerSurname { get; set; }

		[DataMember]
		public DateTime EndAuction { get; set; }

		#endregion

        public AuctionHouse() { }
	}
}
