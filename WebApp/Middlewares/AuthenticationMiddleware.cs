using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebApp.Middlewares
{
    public class AuthenticationMiddleware
    {
        private RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;
            if (path.HasValue)
            {
                if (!path.Value.StartsWith("/Authenticate"))
                {
                    var session = context.Session.GetString("Username");
                    if (session == null)
                    {
                        context.Response.Redirect("/Authenticate/Login");
                    }
                    else
                    {
                        var role = context.Session.GetString("Role");
                        if (path.Value.ToLower().StartsWith("/Supplies".ToLower())
                            || path.Value.ToLower().StartsWith("/Suppliers".ToLower())
                            || path.Value.ToLower().StartsWith("/Vouchers".ToLower())
                            || path.Value.ToLower().StartsWith("/Staff/Create".ToLower())
                            || path.Value.ToLower().StartsWith("/Staff/Delete".ToLower())
                        )
                        {
                            if (!role.Equals("Admin"))
                            {
                                context.Response.Redirect("/Error");
                            }
                        }
                        else if(path.Value.ToLower().StartsWith("/Index".ToLower()))
						{
                            if (!role.Equals("Staff"))
							{
                                context.Response.Redirect("/Error");
                            }
						}
                    }
                }
            }

            await _next(context);

        }
    }

    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
