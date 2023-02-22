using System.Net;

namespace FootballApp.Services.Models
{
    public class TechnialError : BaseError
    {
        public TechnialError() : this(null!)
        {

        }

        public TechnialError(string message) : this(message, HttpStatusCode.InternalServerError)
        {

        }

        public TechnialError(string message, HttpStatusCode statusCode) : base(nameof(TechnialError), message)
        {
            StatusCode = statusCode;
        }
    }
}
