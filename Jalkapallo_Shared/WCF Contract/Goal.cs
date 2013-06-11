using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Jalkapallo_Shared
{
	public enum GoalType
	{
		None,
		Rigore,
		Punizione,
		Testa,
		Tiro,
		Autogol
	}

	[DataContract]
	public class Goal
	{
		[DataMember]
		public int MinutesGoal { get; set; }

		[DataMember]
		public Player Markers { get; set; }

		[DataMember]
		public Team Team { get; set; }

		[DataMember]
		public GoalType GoalType { get; set; }

		public Goal() { }
	}
}
