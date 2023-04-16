using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Core.AppExceptions
{
    public class ErrorMessage
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }


    public class AppException : Exception
    {
        public IEnumerable<ErrorMessage> Errors { get; set; } = new List<ErrorMessage>();
        public int Code { get; set; } = 400;

        public AppException(string message) : base(message)
        {

        }

        public AppException(string message, int code) : this(message)
        {
            this.Code = code;
        }

        public AppException(string message, IEnumerable<ErrorMessage> errors) : this(message, 400)
        {
            this.Errors = errors;

        }
    }
}
