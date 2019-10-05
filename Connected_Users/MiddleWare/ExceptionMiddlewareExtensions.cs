using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connected_Users.MiddleWare
{
    public static  class ExceptionMiddlewareExtensions
    {

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                       // logger.LogError($"Something went wrong: {contextFeature.Error}");

                        var anonymousObject = new {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error." + contextFeature.Error,
                        };
                        await context.Response.WriteAsync(anonymousObject.ToString());
                    }
                });
            });
        }
    }
}
