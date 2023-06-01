namespace WebApplication1.Middlewares.RequestRateLimit;

public static class RequestRateLimitMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestRateLimitMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestRateLimitMiddleware>();
    }
}