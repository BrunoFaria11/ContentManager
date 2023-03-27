using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Common.Interfaces.Repositories;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;

namespace Timelogger.Common.Services
{
    public class TimerHistoryService : ITimerHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TimerHistoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TimerHistory> AddTimerHistory(TimerHistory timerHistory, CancellationToken cancellationToken)
        {
            var newTimerHistory = await _unitOfWork.TimerHistoryRepository.AddAsync(timerHistory, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return newTimerHistory;
        }

        public async Task<List<TimerHistory>> GetAllTimerHistory(bool isCompleted, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TimerHistoryRepository.FindAllAsync(x => x.Id != "", cancellationToken);
        }

        public async Task<TimerHistory> GetTimerHistory(string id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TimerHistoryRepository.FindAsync(x => x.Id == id, cancellationToken);
        }


        public async Task<TimerHistory> UpdateTimerHistory(TimerHistory timerHistory, CancellationToken cancellationToken)
        {
            var newTimerHistory = await _unitOfWork.TimerHistoryRepository.UpdateAsync(timerHistory, timerHistory.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return newTimerHistory;
        }
    }
}

