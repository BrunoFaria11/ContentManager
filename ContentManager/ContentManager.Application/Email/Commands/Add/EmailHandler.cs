using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ContentManager.Application.Common.Models;
using ContentManager.Common.Interfaces.Services;
using ContentManager.Commands;
using ContentManager.Application.Common.Exceptions;
using ContentManager.Common.Interfaces.Repositories;
using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;

namespace ContentManager.Commands
{
    public class EmailHandler : IRequestHandler<EmailCommand, Response<bool>> 
    {


        public EmailHandler()
        {
        }

        public async Task<Response<bool>> Handle(EmailCommand request, CancellationToken cancellationToken)
        {
            MailjetClient client = new MailjetClient("0a90f0c4523bd31bb8ef7f7e54b92b5b","5d81ef732459fdb802008a1ccf013203");
          
            MailjetRequest _request = new MailjetRequest()
            {
                Resource = Send.Resource,
            }
                .Property(Send.FromEmail, request.FromEmail)
                .Property(Send.FromName, request.FromName)
                .Property(Send.Recipients, new JArray {
                 new JObject {
                     {"Email",request.ToEmail},
                     {"Name", request.ToName}
                 }
                })
                .Property(Send.Subject, request.Subject)
                .Property(Send.TextPart, request.TextPart)
                .Property(Send.HtmlPart, request.Body);

            MailjetResponse response = await client.PostAsync(_request);

            return new Response<bool>(true);
        }
    }
}

