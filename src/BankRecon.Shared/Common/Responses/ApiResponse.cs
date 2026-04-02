namespace BankRecon.Shared.Common.Responses;

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public T? Result { get; set; }
    public List<string>? Errors { get; set; }

    public static ApiResponse<T> Success(T result, string message = "Success")
    {
        return new ApiResponse<T>
        {
            IsSuccess = true,
            Message = message,
            Result = result
        };
    }

    public static ApiResponse<T> Failure(string message, List<string>? errors = null)
    {
        return new ApiResponse<T>
        {
            IsSuccess = false,
            Message = message,
            Errors = errors ?? new List<string>()
        };
    }
}

public class ApiResponse
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }

    public static ApiResponse Success(string message = "Success")
    {
        return new ApiResponse
        {
            IsSuccess = true,
            Message = message
        };
    }

    public static ApiResponse Failure(string message, List<string>? errors = null)
    {
        return new ApiResponse
        {
            IsSuccess = false,
            Message = message,
            Errors = errors ?? new List<string>()
        };
    }
}