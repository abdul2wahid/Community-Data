using DAL;
using DAL.Entities;

using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer
{
    public class Utility_BL
    {
        Utility_DAL dal;


        public Utility_BL()
        {
            dal = new Utility_DAL();
        }

        public List<Roles> GetRoles(string loggedInUserRoleId, string userId)
        {
             List<Roles> result = dal.GetRoles();
            if (Convert.ToInt32(loggedInUserRoleId) != 0) //except super user
            {
                result.RemoveAll(x => x.RoleId <= Convert.ToInt32(loggedInUserRoleId));
            }
           

            return result;
        }
    }
}
