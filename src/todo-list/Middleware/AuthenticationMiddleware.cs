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
            var email = httpContext.Request.Body;
            return _next(httpContext);
        }
  }
}