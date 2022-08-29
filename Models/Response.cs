namespace Models
{
    public class Response
    {
        public int Status { get; set; }

        public bool IsError { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }

        public Response(int status, bool isError, string message, object result)
        {
            Status = status;
            IsError = isError;
            Message = message;
            Result = result;
        }
    }
}
