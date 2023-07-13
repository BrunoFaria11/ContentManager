using System;

namespace Timelogger.Entities
{
	public class TimerHistory
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
		public string ProjectId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public double? TotalHours { get; set; }
	}
}
