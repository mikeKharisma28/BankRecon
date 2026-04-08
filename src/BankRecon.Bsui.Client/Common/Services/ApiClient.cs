using System.Net.Http.Json;
using BankRecon.Bsui.Client.Common.Interfaces;
using BankRecon.Bsui.Client.Common.Options;
using BankRecon.Shared.Common.Responses;
using Microsoft.Extensions.Options;

namespace BankRecon.Bsui.Client.Common.Services;

/// <summary>
/// Base HTTP API client implementation.
/// Handles JSON serialization/deserialization and common HTTP operations.
/// </summary>
public class ApiClient : IApiClient
{
    private readonly HttpClient _httpClient;
    private readonly BackEndOptions _options;

    public ApiClient(HttpClient httpClient, IOptions<BackEndOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;

        _httpClient.BaseAddress = new Uri(_options.BaseUrl);
    }

    /// <inheritdoc />
    public async Task<ApiResponse<T>> GetAsync<T>(
        string endpoint,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<T>>(
            endpoint, cancellationToken);

        return response ?? ApiResponse<T>.Failure("Failed to deserialize API response.");
    }

    /// <inheritdoc />
    public async Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        var httpResponse = await _httpClient.PostAsJsonAsync(
            endpoint, request, cancellationToken);

        var response = await httpResponse.Content
            .ReadFromJsonAsync<ApiResponse<TResponse>>(cancellationToken: cancellationToken);

        return response ?? ApiResponse<TResponse>.Failure("Failed to deserialize API response.");
    }

    /// <inheritdoc />
    public async Task<ApiResponse<TResponse>> PutAsync<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        var httpResponse = await _httpClient.PutAsJsonAsync(
            endpoint, request, cancellationToken);

        var response = await httpResponse.Content
            .ReadFromJsonAsync<ApiResponse<TResponse>>(cancellationToken: cancellationToken);

        return response ?? ApiResponse<TResponse>.Failure("Failed to deserialize API response.");
    }

    /// <inheritdoc />
    public async Task<ApiResponse<T>> DeleteAsync<T>(
        string endpoint,
        CancellationToken cancellationToken = default)
    {
        var httpResponse = await _httpClient.DeleteAsync(endpoint, cancellationToken);

        var response = await httpResponse.Content
            .ReadFromJsonAsync<ApiResponse<T>>(cancellationToken: cancellationToken);

        return response ?? ApiResponse<T>.Failure("Failed to deserialize API response.");
    }
}
