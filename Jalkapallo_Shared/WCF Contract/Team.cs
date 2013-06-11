using System;
using System.Net;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Jalkapallo_Shared
{
	//[XmlSerializerFormat]
	[DataContract]
	public class Team
	{
		//[DataMember, XmlAttribute]
		[DataMember]
		public string Name { get; set; }

		//[DataMember, XmlAttribute]
		[DataMember]
		public List<Player> TeamPlayers { get; set; }

		//[DataMember, XmlAttribute]
		[DataMember]
		public List<Match> Matches { get; set; }

		//[DataMember, XmlAttribute]
		[DataMember]
		public Coach Coach { get; set; }

		//[DataMember, XmlAttribute]
		[DataMember]
		public double Budget { get; set; }

		//[DataMember, XmlAttribute]
		[DataMember]
		public int Formation { get; set; }

		//[DataMember, XmlAttribute]
		[DataMember]
		public Guid League { get; set; }

		public Team() { }

		public Team(string teamName, int initialFormation)
		{
			Name = teamName;
			Formation = initialFormation;
		}
	}
}
