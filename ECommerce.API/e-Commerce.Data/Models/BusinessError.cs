using System.Net;

namespace FootballApp.Services.Models
{
    public class BusinessError : BaseError
    {
        public BusinessError() : this(null!)
        {

        }

        public BusinessError(string message) : this(message, HttpStatusCode.BadRequest)
        {

        }

        public BusinessError(string message, HttpStatusCode statusCode) : base(nameof(BusinessError), message)
        {
            StatusCode = statusCode;
        }
    }
}
