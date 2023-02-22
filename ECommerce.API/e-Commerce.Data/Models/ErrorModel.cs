namespace FootballApp.Services.Models
{
    public class ErrorModel
    {
        public ErrorModel()
        {

        }

        public ErrorModel(Exception exception) : this()
        {
            Message = exception.Message;
            Type = exception is BaseError baseError ? baseError.Type : nameof(TechnialError);

            if(exception.InnerException != null)
            {
                InnerError = new ErrorModel(exception.InnerException);
            }
        }

        public string Message { get; set; }
        public ErrorModel InnerError { get; set; }
        public string Type { get; set; }
    }
}
