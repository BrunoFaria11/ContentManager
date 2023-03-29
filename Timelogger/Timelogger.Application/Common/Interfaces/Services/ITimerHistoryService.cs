using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Entities;

namespace Timelogger.Common.Interfaces.Services
{
    public interface ITimerHistoryService
    {
        Task<TimerHistory> AddTimerHistory(TimerHistory timerHistory, CancellationToken cancellationToken);
        Task<List<TimerHistory>> GetAllTimerHistory(string projectId, CancellationToken cancellationToken);
        Task<TimerHistory> GetTimerHistory(string id, CancellationToken cancellationToken);
        Task<TimerHistory> UpdateTimerHistory(TimerHistory timerHistory, CancellationToken cancellationToken);
    }
}

