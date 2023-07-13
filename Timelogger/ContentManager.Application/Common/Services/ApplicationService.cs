using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ContentManager.Common.Interfaces.Repositories;
using ContentManager.Common.Interfaces.Services;

namespace ContentManager.Common.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}

