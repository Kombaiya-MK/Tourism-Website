namespace KTWFeedbackAPI.Models
{
    public class Error
    {
        public int ID { get; set; }
        public string? Message { get; set; }

        public Error(int id, string message)
        {
            ID = id;
            Message = message;
        }

        public enum ErrorCode : int
        {
            NotFound = 404,
            BadRequest = 400,
        }

    }
}
