using System.Net;
using Microsoft.Extensions.Caching.Distributed;

namespace WebApplication1.Middlewares.RequestRateLimit;

public class RequestRateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly int _maxRequestsPerMinute;

    public RequestRateLimitMiddleware(RequestDelegate next, int maxRequestsPerMinute = 5)
    {
        _next = next;
        _maxRequestsPerMinute = maxRequestsPerMinute;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        // Implement rate limiting logic here
        
        var count = 0;
        var ipAddress = httpContext.Connection.RemoteIpAddress;
        var cacheKey = $"{ipAddress}:{DateTime.UtcNow:yyyy-MM-dd--HH-mm}";
        var cacheEntry = await httpContext.RequestServices.GetRequiredService<IDistributedCache>().GetStringAsync(cacheKey);
        if (cacheEntry != null)
        {
            count = int.Parse(cacheEntry);
        }
        else
        {
            await httpContext.RequestServices.GetRequiredService<IDistributedCache>().SetStringAsync(cacheKey, count.ToString(), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });
        }
        
        if (count > _maxRequestsPerMinute)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
            await httpContext.Response.WriteAsync("Too many requests");
            return;
        }
        
        await httpContext.RequestServices.GetRequiredService<IDistributedCache>().SetStringAsync(cacheKey, (count + 1).ToString(), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        });
        
        await _next(httpContext);
    }
}