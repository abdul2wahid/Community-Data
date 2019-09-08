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


        public List<City> GetCities()
        {
            using (var context = new connected_usersContext())
            {

                return context.City.ToList();
            }
        }

        public List<States> GetStates()
        {
            using (var context = new connected_usersContext())
            {

                return context.States.ToList();
            }
        }

        public List<Pincode> GetPincodes()
        {
            using (var context = new connected_usersContext())
            {

                return context.Pincode.ToList();
            }
        }

        public List<Education> GetEducation()
        {
            using (var context = new connected_usersContext())
            {

                return context.Education.ToList();
            }
        }

        public List<Arabiceducation> GetArabicEducation()
        {
            using (var context = new connected_usersContext())
            {

                return context.Arabiceducation.ToList();
            }
        }



    }
}
