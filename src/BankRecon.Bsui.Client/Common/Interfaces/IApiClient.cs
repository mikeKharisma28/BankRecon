using BankRecon.Shared.Common.Responses;

namespace BankRecon.Bsui.Client.Common.Interfaces;

/// <summary>
/// Base interface for HTTP API client operations.
/// Provides common methods for communicating with the WebApi layer.
/// </summary>
public interface IApiClient
{
    /// <summary>
    /// Sends a GET request and deserializes the response.
    /// </summary>
    Task<ApiResponse<T>> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a POST request with a JSON body and deserializes the response.
    /// </summary>
    Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a PUT request with a JSON body and deserializes the response.
    /// </summary>
    Task<ApiResponse<TResponse>> PutAsync<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a DELETE request and deserializes the response.
    /// </summary>
    Task<ApiResponse<T>> DeleteAsync<T>(string endpoint, CancellationToken cancellationToken = default);
}
