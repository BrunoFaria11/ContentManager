using System;
using ContentManager.Application.Common.Models;
using MediatR;

namespace ContentManager.Commands
{
    public class EmailCommand :  IRequest<Response<bool>>
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Subject { get; set; }
        public string TextPart { get; set; }
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string Body { get; set; }

    }
}

