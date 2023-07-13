using Microsoft.EntityFrameworkCore;
using ContentManager.Domain.Entities;
using ContentManager.Persistance;
using System.Reflection;

namespace ContentManager
{
    public class CMDbContext : DbContext, ICMDbContext
    {

        public CMDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<ContentManager.Domain.Entities.Application> Application { get; set; }
        public DbSet<Models> Models { get; set; }
        public DbSet<Users> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
