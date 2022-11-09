using todo_list.Middleware;

namespace todo_list.Extension
{
  public static class ExceptionMiddlewareExtesions
    {
        public static IApplicationBuilder UseExeptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}