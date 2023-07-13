using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ContentManager.Application.Common.Models;
using ContentManager.Domain.Entities;
using ContentManager.Common.Interfaces.Repositories;
using ContentManager.Application.Common.Exceptions;
using System.Linq;

namespace ContentManager.Commands
{
    public class GetAllTimerHistoryHandler : IRequestHandler<GetAllModelsCommand, Response<List<Models>>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetAllTimerHistoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<List<Models>>> Handle(GetAllModelsCommand request, CancellationToken cancellationToken)
       {
            var application = await _unitOfWork.ApplicationRepository.FindAsync(x => x.Uuid == request.ApplicationId, cancellationToken);
            if (application == null)
            {
                throw new ApiException($"Project doesn´t exist");
            }

            var response = await _unitOfWork.ModelRepository.FindAllAsync(x=>x.Name == request.Name && x.ApplicationId == application.Id, cancellationToken);
           return new Response<List<Models>>(response.OrderByDescending(x => x.CreationDate).ToList());
       }
    }
}

