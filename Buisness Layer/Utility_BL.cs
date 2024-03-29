﻿using DAL;
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
                result.RemoveAll(x => x.RoleId < Convert.ToInt32(loggedInUserRoleId));
            }
           

            return result;
        }

        public List<Gender> GetGender(string loggedInUserRoleId, string userId)
        {
            return dal.GetGender();
        }

        public List<Maritalstatus> GetMaritalstatus(string loggedInUserRoleId, string userId)
        {
            return dal.GetMaritalstatus();
        }


        public List<Occupation> GetOccupation(string loggedInUserRoleId, string userId)
        {
            return dal.GetOccupation();
        }


        public List<City> GetCities(string loggedInUserRoleId, string userId)
        {
            return dal.GetCities();
        }


        public List<States> GetStates(string loggedInUserRoleId, string userId)
        {
            return dal.GetStates();
        }


        public List<Pincode> GetPincodes(string loggedInUserRoleId, string userId)
        {
            return dal.GetPincodes();
        }


        public List<Education> GetEducation(string loggedInUserRoleId, string userId)
        {
            return dal.GetEducation();
        }

        public List<Arabiceducation> GetArabicEducation(string loggedInUserRoleId, string userId)
        {
            return dal.GetArabicEducation();
        }

    }
}
