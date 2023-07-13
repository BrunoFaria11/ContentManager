using System;
namespace ContentManager.Domain.Entities
{
    public class Models
    {
		public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public bool Active { get; set; }

        public Models()
        {
        }
    }
}

