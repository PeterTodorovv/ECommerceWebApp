namespace FootballApp.Services.Models
{
    public class ApiResponse<T>
    {
        public int Status { get; set; }
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
        public T Result { get; set; }
    }
}
