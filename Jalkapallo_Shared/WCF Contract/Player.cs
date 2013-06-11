using System;
using System.Net;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Jalkapallo_Shared
{
	[DataContract(Name = "RolePlayer")]
	public enum RolePlayerEnum
	{
		[EnumMember]
		Portiere = 0,
		[EnumMember]
		Difensore = 1,
		[EnumMember]
		CentroCampista = 2,
		[EnumMember]
		Attaccante = 3,
		[EnumMember]
		Allenatore = 4
	}

	[DataContract]
	public class Player
	{
		#region Properties

		[DataMember]
		public Guid Guid { get; set; }

		[DataMember]
		public string Country { get; set; }

		[DataMember]
		public float Weight { get; set; }

		[DataMember]
		public int Height { get; set; }

		[DataMember]
		public string Birthday { get; set; }

		[DataMember]
		public RolePlayerEnum Role { get; set; }

		[DataMember]
		public PlayerSkills Skills { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Surname { get; set; }

		[DataMember]
		public string TeamName { get; set; }	// TASK:0006 ++ 20130412 DB

        [DataMember]
        public int Stipendio { get; set; } 

		#endregion

		public Player()
		{
			// TODO: Complete member initialization
		}
	}
}
