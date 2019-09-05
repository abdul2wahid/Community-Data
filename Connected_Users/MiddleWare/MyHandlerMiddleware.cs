using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Connected_Users.MiddleWare
{
    public class MyHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        public MyHandlerMiddleware(RequestDelegate next)

        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            context.User = GenerateResponse(context);

            await _next.Invoke(context);

        }

        // ...

        private ClaimsPrincipal GenerateResponse(HttpContext context)
        {
            try
            {
                string token = context.Request.Headers["Token"];
                if (token != null)
                {
                    TokenManager.Token tokenManager = new TokenManager.Token();
                    ClaimsPrincipal principal = tokenManager.ValidateToken(token);
                    return principal;
                }

              
            }
            catch(Exception ex)
            {
                //log here
            }
            return null;
        }

    }

    public static class MyHandlerExtensions
    {
        public static IApplicationBuilder UseMyHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyHandlerMiddleware>();
        }
    }
}
