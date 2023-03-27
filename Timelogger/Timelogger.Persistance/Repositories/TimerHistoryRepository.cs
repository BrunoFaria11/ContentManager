using System;
using Timelogger.Common.Interfaces.Repositories;
using Timelogger.Entities;

namespace Timelogger.Repositories
{
    public class TimerHistoryRepository : EfRepository<TimerHistory>, ITimerHistoryRepository
    {
        private readonly ApiContext _context;

        public TimerHistoryRepository(ApiContext context) : base(context)
        {
            _context = context;
        }

    }
}

