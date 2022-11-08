using Microsoft.AspNetCore.Authorization;
using todo_list.Domain.Service;

namespace todo_list.Middleware
{
  public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var endpoint = httpContext.GetEndpoint();

            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is object)
            {
                return _next(httpContext);
            }

            var authService = httpContext
                .RequestServices
                .GetService<IAuthService>();
            
            if(authService == null)
                throw new ArgumentException("Auth service unavailable");

            if(!authService.IsTokenValid(httpContext.Request.Headers.Authorization))
                throw new HttpRequestException("Invalid authorization");

            return _next(httpContext);
        }
        
  }
}