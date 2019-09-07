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

        public List<Gender> GetGender()
        {
            using (var context = new connected_usersContext())
            {

                return context.Gender.ToList();
            }
        }


        public List<Maritalstatus> GetMaritalstatus()
        {
            using (var context = new connected_usersContext())
            {

                return context.Maritalstatus.ToList();
            }

        }

        public List<Occupation> GetOccupation()
        {
            using (var context = new connected_usersContext())
            {

                return context.Occupation.ToList();
            }
        }
    }
}
