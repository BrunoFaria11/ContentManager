using System;
using System.Collections.Generic;

namespace Timelogger.Entities
{
	public class Invoice
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
		public string DevName { get; set; }
		public string DevDocNumber { get; set; }

		private string invoiceNumber { get; set; }
		public string InvoiceNumber
		{
			get
			{
				if (this.invoiceNumber == null)
				{
					this.invoiceNumber = Guid.NewGuid().ToString();
				}
				return this.invoiceNumber;
			}
			set => this.invoiceNumber = value;
		}

		public DateTime InvoiceDate { get; set; }

		public string CustomerName { get; set; }
		public string CustomerDocNumber { get; set; }

	}
}