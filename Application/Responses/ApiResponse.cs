using System.Net;

namespace Application.Responses;

public sealed class ApiResponse<T>
{
    public HttpStatusCode StatusCode { get; init; }
    public string Message { get; init; }
    public T? Data { get; init; }

    public ApiResponse(HttpStatusCode statusCode, string message, T? data = default)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }
}
