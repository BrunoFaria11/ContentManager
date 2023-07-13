using System;
using MediatR;
using ContentManager.Application.Common.Models;
using ContentManager.Domain.Entities;

namespace ContentManager.Commands
{
    public class ModelCommand :  IRequest<Response<Models>>
    {
        public string ApplicationId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}

