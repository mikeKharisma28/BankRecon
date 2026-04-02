using BankRecon.Shared.Common.Responses;

namespace BankRecon.Shared.Common.Extensions;

public static class ExceptionExtensions
{
    public static ErrorResponse ToErrorResponse(this Exception ex)
    {
        return new ErrorResponse
        {
            Message = ex.Message,
            Details = ex.StackTrace ?? string.Empty
        };
    }
}