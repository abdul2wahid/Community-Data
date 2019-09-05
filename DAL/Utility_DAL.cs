using DAL.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class Utility_DAL
    {


        public Utility_DAL()
        {
        }


        public List<Roles> GetRoles()
        {
            using (var context = new connected_usersContext())
            {

                return context.Roles.ToList();
            }
        }
    }
}
