using System;
using System.Collections.Generic;

namespace Timelogger.Entities
{
	public class Project
	{
		private string id { get; set; }
		public string Id
		{
			get
			{
				if (this.id == null)
				{
					this.id = Guid.NewGuid().ToString();
				}
				return this.id;
			}
			set => this.id = value;
		}
		public string Name { get; set; }
		public DateTime DeadLine { get; set; }
		public decimal TimePerWeek { get; set; }
		public double TotalTimeSpent { get; set; }
		public bool IsCompleted { get; set; }
	}
}