using MediatR;
using Microsoft.Extensions.Logging;

namespace BankRecon.Application.Common.Behaviors;

/// <summary>
/// MediatR pipeline behavior that logs request details
/// for diagnostics and observability.
/// </summary>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogInformation("Handling {RequestName} {@Request}", requestName, request);

        TResponse response = await next();

        _logger.LogInformation("Handled {RequestName}", requestName);

        return response;
    }
}
