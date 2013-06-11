using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Jalkapallo_Shared
{
	[DataContract]
	public class SearchInfo
	{
		[DataMember]
		//////////////// TeamName - Players
		public Dictionary<string, List<string>> teamAndPlayers { get; set; }
	}
}
