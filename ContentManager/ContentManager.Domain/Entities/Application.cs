using System;
namespace ContentManager.Domain.Entities
{
    public class Application
    {
        public int Id { get; set; }

		private string uuid { get; set; }
		public string Uuid
		{
			get
			{
				if (this.uuid == null)
				{
					this.uuid = Guid.NewGuid().ToString();
				}
				return this.uuid;

            }
			set => this.uuid = value;
		}

		public string Name { get; set; }

		public Application()
        {
          

        }
    }
}

