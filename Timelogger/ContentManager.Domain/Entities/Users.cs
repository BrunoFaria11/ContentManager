using System;
namespace ContentManager.Domain.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        

        public Users()
        {
        }
    }
}

