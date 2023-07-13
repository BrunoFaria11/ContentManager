using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ContentManager.Application.Common.Models;
using ContentManager.Common.Interfaces.Services;
using ContentManager.Commands;
using ContentManager.Application.Common.Exceptions;
using ContentManager.Common.Interfaces.Repositories;

namespace ContentManager.Commands
{
    public class ApplicationHandler : IRequestHandler<ApplicationCommand, Response<Domain.Entities.Application>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public ApplicationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Domain.Entities.Application>> Handle(ApplicationCommand request, CancellationToken cancellationToken)
        {

            if (await _unitOfWork.ApplicationRepository.FindAsync(x=>x.Name == request.Name,cancellationToken) != null)
            {
                throw new ApiException($"Project already exist");
            }

            Domain.Entities.Application Application = new Domain.Entities.Application()
            {
                Name = request.Name,
            };

            var response = await _unitOfWork.ApplicationRepository.AddAsync(Application, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Response<Domain.Entities.Application>(response);
        }
    }
}

