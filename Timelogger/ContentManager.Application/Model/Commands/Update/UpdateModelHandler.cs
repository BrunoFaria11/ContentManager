using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ContentManager.Application.Common.Exceptions;
using ContentManager.Application.Common.Models;
using ContentManager.Common.Interfaces.Services;
using ContentManager.Domain.Entities;
using ContentManager.Common.Interfaces.Repositories;

namespace ContentManager.Commands
{
    public class UpdateModelHandler : IRequestHandler<UpdateModelCommand, Response<Models>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public UpdateModelHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Models>> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.ModelRepository.FindAsync(x => x.Id == request.Id, cancellationToken);
            if (model == null)
            {
                throw new ApiException($"Project doesn´t exist");
            }

            model.Value = request.Value;

            var response = await _unitOfWork.ModelRepository.UpdateAsync(model, model.Id ,cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Response<Models>(response);
        }
    }
}

