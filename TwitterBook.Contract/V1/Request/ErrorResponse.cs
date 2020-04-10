using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterBook.Contracts.V1.Request
{
    public class ErrorResponse
    {
        public ErrorResponse() { }

        public ErrorResponse(ErrorMessage error)
        {
            Errors.Add(error);
        }

        public List<ErrorMessage> Errors { get; set; } = new List<ErrorMessage>();
    }

    public class ErrorMessage
    {
        public string FieldName { get; set; }

        public string Message { get; set; }
    }
}
