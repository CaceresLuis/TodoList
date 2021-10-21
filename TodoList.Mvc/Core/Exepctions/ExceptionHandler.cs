using System;
using System.Net;

namespace TodoList.Mvc.Core.Exepctions
{
    public class ExceptionHandler : Exception
    {
        public HttpStatusCode Code { get; }
        public Error Error { get; }
        public ExceptionHandler(HttpStatusCode code, Error error)
        {
            Code = code;
            Error = error;
        }
    }
}
