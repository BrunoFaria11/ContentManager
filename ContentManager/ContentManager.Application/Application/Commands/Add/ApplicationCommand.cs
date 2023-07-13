using System;
using ContentManager.Application.Common.Models;
using MediatR;

namespace ContentManager.Commands
{
    public class ApplicationCommand :  IRequest<Response<ContentManager.Domain.Entities.Application>>
    {
        public string Name { get; set; }
    }
}

