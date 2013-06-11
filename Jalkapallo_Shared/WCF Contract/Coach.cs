using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Jalkapallo_Shared
{
	[DataContract]
	public class Coach : Player
	{
		[DataMember]
		public int CoachAbility { get; set; }
		[DataMember]
		public string CoachName { get; set; }
		[DataMember]
		public string CoachSurname { get; set; }
		[DataMember]
		public PlayerSkills CoachGenericSkills { get; set; }
		[DataMember]
		public string TrainingType { get; set; }

		public Coach()
			: base()
		{
			CoachAbility = 0;
			CoachName = string.Empty;
			CoachSurname = string.Empty;
		}
	}
}
