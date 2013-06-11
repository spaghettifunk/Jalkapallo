using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobSchedulerWR
{
	public class Task
	{
		public DateTime MatchTime { get; private set; }
		public DateTime TrainingTime { get; private set; }
		public DateTime PurchaseTime { get; private set; }
		public DateTime BudgetTime { get; private set; }

		public Task()
		{
			DateTime now = DateTime.UtcNow;
			MatchTime = new DateTime(now.Year, now.Month, now.Day, 15, 15, 0);
			TrainingTime = new DateTime(now.Year, now.Month, now.Day, 7, 15, 0);
			PurchaseTime = new DateTime(now.Year, now.Month, now.Day, 23, 15, 0);
			BudgetTime = new DateTime(now.Year, now.Month, now.Day, 1, 0, 0);
		}
	}
}