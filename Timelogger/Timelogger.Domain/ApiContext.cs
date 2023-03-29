using Microsoft.EntityFrameworkCore;
using Timelogger.Entities;

namespace Timelogger
{
    public interface IApiContext
    {

        DbSet<Project> Projects { get; set; }
        DbSet<TimerHistory> TimerHistory { get; set; }
        DbSet<Invoice> Invoice { get; set; }
    }

    public class ApiContext : DbContext, IApiContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TimerHistory> TimerHistory { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
    }
}
