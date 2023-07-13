using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ContentManager.Common.Interfaces.Repositories;
using ContentManager.Common.Interfaces.Services;
using ContentManager.Domain.Entities;

namespace ContentManager.Common.Services
{
    public class ModelsService : IModelsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModelsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}

