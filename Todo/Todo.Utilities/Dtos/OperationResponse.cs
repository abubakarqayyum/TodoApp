namespace CommonService.Dtos
{
    public class OperationResponse
    {
        public bool HasSucceeded { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Result { get; set; }

        public OperationResponse() { }

        public OperationResponse(bool hasSucceeded, int statusCode, string? message, object? result)
        {
            HasSucceeded = hasSucceeded;
            StatusCode = statusCode;
            Message = message;
            Result = result;
        }
    }
}



