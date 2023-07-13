using System;
using MediatR;
using ContentManager.Application.Common.Models;
using ContentManager.Domain.Entities;

namespace ContentManager.Commands
{
    public class UpdateModelCommand : IRequest<Response<Models>>
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}

