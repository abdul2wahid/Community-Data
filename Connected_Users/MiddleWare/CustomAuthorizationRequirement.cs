using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connected_Users.MiddleWare
{
    public class CustomAuthorizationRequirement : IAuthorizationRequirement
    {
        public CustomAuthorizationRequirement(List<string> roles)
        {
            Roles = roles;
        }

        public List<string> Roles { get; private set; }
    }  
   
}
