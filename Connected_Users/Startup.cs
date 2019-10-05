using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Connected_Users.MiddleWare;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace Connected_Users
{
    public class Startup
    {


       public static int PageSize = 10;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            services.AddCors(); 


            services.AddMvc();

 

            // Authorization and authentication are closely linked in ASP.NET Core. When authorization fails, this will be passed to an authentication handler to handle the authorization failure.
            //So even if you don’t need actual authentication to identify your users, you will still need to set up some authentication scheme that is able to handle forbid and challenge results(403 and 401).
            //To do that, you need to call AddAuthentication() and configure a default forbid / challenge scheme:
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = "scheme name";

                // you can also skip this to make the challenge scheme handle the forbid as well
                options.DefaultForbidScheme = "scheme name";

                // of course you also need to register that scheme, e.g. using
                options.AddScheme<CustomAuthenticationHandler>("scheme name", "scheme display name");
            });

            //services.AddAuthorization(options =>
            //{

            //    options.AddPolicy("WebSiteAdminPolicy",
            //        policy =>
            //        policy.RequireClaim("Role", "SUPER_USER","ADMIN")
            //       );



            //});

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.Requirements.Add(new CustomAuthorizationRequirement(new List<string>() { Constants.Admin })));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AllLoggedInUsersPolicy", policy => policy.Requirements.Add(new CustomAuthorizationRequirement(new List<string>() { Constants.Super_User,Constants.Admin,Constants.Memeber })));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperUser&AdminPolicy", policy => policy.Requirements.Add(new CustomAuthorizationRequirement(new List<string>() { Constants.Super_User, Constants.Admin })));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperUserPolicy", policy => policy.Requirements.Add(new CustomAuthorizationRequirement(new List<string>() { Constants.Super_User})));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MemberPolicy", policy => policy.Requirements.Add(new CustomAuthorizationRequirement(new List<string>() {  Constants.Memeber })));
            });

            services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

         app.UseCors(
         options => options.WithOrigins("http://localhost:4200").AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyMethod());

            app.ConfigureExceptionHandler();

           app.UseAuthentication();
            app.UseMyHandler();
            

            app.UseMvc();

            
        }
    }
}
