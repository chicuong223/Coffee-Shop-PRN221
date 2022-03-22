﻿using Microsoft.AspNetCore.Builder;
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
                            || path.Value.ToLower().StartsWith("/Suppliers".ToLower()))
                        {
                            if (!role.Equals("Admin"))
                            {
                                context.Response.Redirect("/Unauthorized");
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
