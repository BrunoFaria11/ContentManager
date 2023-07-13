using System.Collections.Generic;

namespace ContentManager.Application.Common.Models
{
    public class Response
    {
        public Response(bool succeeded, string message = null)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }

    public class Response<T> : Response
    {
        public Response() : base(true)
        {

        }

        public Response(T data, string message = null) : base(true, message)
        {
            Data = data;
        }

        public T Data { get; set; }

    }
}
