using System;
using System.Net;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Jalkapallo_Shared
{
	[DataContract]
	public class Match
	{
		[DataMember]
		public Team Home { get; set; }

		[DataMember]
		public Team Away { get; set; }

		[DataMember]
		public List<Goal> MatchGoals { get; set; }

		[DataMember]
		public int ResultHome { get; set; }

		[DataMember]
		public int ResultAway { get; set; }

		[DataMember]
		public List<Player> RedCard { get; set; }

		[DataMember]
		public List<Player> YellowCard { get; set; }

		[DataMember]
		public string HomeStringFromServer { get; set; }

		[DataMember]
		public string AwayStringFromServer { get; set; }

		[DataMember]
		public List<string> RedCardStringFromServer { get; set; }

		[DataMember]
		public List<string> YellowCardStringFromServer { get; set; }

		[DataMember]
		public Guid IdCampionato { get; set; }

		public Match() { }
	}
}
