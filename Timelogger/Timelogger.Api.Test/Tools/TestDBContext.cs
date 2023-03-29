using System;
using Microsoft.EntityFrameworkCore;

namespace Timelogger.Api.Test.Tools
{
    public class TestDbContext : ApiContext
    {
        public TestDbContext() : base(new DbContextOptions<ApiContext>())
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString()).EnableSensitiveDataLogging();
        }

        // The following methods MUST NOT call base.Dispose(). This DbContext was created in order to share the same instance between the Test cases and the application.
        // Calling base.Dispose() will mark the dbContext instance as disposed and make it unavailable for the test cases.
        public override void Dispose()
        {
        }

        public void ForceDispose()
        {
            base.Dispose();
        }
    }
}

