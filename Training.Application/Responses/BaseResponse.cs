namespace Training.Application.Responses
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
            Success = true;
        }
        public BaseResponse(string message)
        {
            Success = true;
            Message = message;
        }

        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }
        public BaseResponse(string message, bool success, int? statusCode)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
        }

        public BaseResponse(string message, bool success, int? statusCode, List<string>? validationErrors)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
            ValidationErrors = validationErrors;
        }
        public BaseResponse(string message, bool success, int? statusCode, T data)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
            Data = data;
        }

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? StatusCode { get; set; }
        public T? Data { get; set; }
        public List<string>? ValidationErrors { get; set; }

    }
}
