using System;
using System.Net;
using System.Runtime.Serialization;

namespace Jalkapallo_Shared
{
	[DataContract(Name = "SkillList")]
	public enum SkillListEnum
	{
		[EnumMember]
		parata = 0,
		[EnumMember]
		difesa = 1,
		[EnumMember]
		centrocampo = 2,
		[EnumMember]
		attacco = 3,
		[EnumMember]
		tiro = 4,
		[EnumMember]
		velocita = 5,
		[EnumMember]
		exp = 6
	}

	[DataContract]
	public class PlayerSkills
	{
		[DataMember]
		public float Parata { get; set; }

		[DataMember]
		public float Difesa { get; set; }

		[DataMember]
		public float Centrocampo { get; set; }

		[DataMember]
		public float Attacco { get; set; }

		[DataMember]
		public float Velocita { get; set; }

		[DataMember]
		public float Tiro { get; set; }

		[DataMember]
		public float Exp { get; set; }

		public PlayerSkills() { }
	}
}
