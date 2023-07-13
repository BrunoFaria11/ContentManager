using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ContentManager.Application.Common.Models;
using ContentManager.Common.Interfaces.Services;
using ContentManager.Commands;
using ContentManager.Application.Common.Exceptions;
using System;
using System.Linq;
using ContentManager.Domain.Entities;
using ContentManager.Common.Interfaces.Repositories;

namespace ContentManager.Commands
{
    public class ModelHandler : IRequestHandler<ModelCommand, Response<Models>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public ModelHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Models>> Handle(ModelCommand request, CancellationToken cancellationToken)
        {
            var application = await _unitOfWork.ApplicationRepository.FindAsync(x => x.Uuid == request.ApplicationId, cancellationToken);
            if (application == null) {
                throw new ApiException($"Project doesn´t exist");
            }

            Models model = new Models()
            {
                ApplicationId = application.Id,
                Value = request.Value,
                CreationDate = DateTime.Now,
                Name = request.Name,
                Active = true,
            };

            var response = await _unitOfWork.ModelRepository.AddAsync(model, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Response<Models>(response);
        }
    }
}

