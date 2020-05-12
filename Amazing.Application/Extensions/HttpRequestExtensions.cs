using Microsoft.AspNetCore.Http;

namespace Amazing.Application.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetHeaderValue(this HttpRequest request, string key)
            => request.Headers[key].Count > 0 ? (string)request.Headers[key] : string.Empty;
    }
}