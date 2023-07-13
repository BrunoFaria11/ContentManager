using System;
using System.Runtime.Serialization;

namespace ContentManager.Application.Common.Exceptions
{

    [Serializable]
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message) { }

        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}

