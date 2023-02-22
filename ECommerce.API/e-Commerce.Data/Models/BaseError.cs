using System.Net;

namespace FootballApp.Services.Models

{
    public class BaseError : Exception
    {
        public BaseError(string type)
        {
            Type = type;
        }

        public BaseError(string type, string message) : base(message)
        {
            Type = type;
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Type { get; set; }
    }
}
