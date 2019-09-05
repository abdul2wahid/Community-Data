using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connected_Users.MiddleWare
{
    public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "Role"))
            {
                return Task.FromResult(0);
            }

            string role = context.User.FindFirst(c => c.Type == "Role").Value;

           
            if (requirement.Roles.Contains(role))
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }
    }
}
