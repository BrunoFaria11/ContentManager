using System;
using System.Collections.Generic;
using MediatR;
using ContentManager.Application.Common.Models;
using ContentManager.Domain.Entities;

namespace ContentManager.Commands
{
    public class GetAllModelsCommand :  IRequest<Response<List<Models>>>
    {
        public string ApplicationId { get; set; }
        public string Name { get; set; }
    }
}

