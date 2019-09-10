using DAL;
using DAL.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Buisness_Layer
{
    public class Customer_BL
    {

        Customer_DAL dal;

        public Customer_BL()
        {
            dal = new Customer_DAL();
        }

        public List<CustomerModel> GetCustomers( string sortOrder, int currentPageNo, string filterString)
        {
           return dal.GetCustomers(sortOrder,currentPageNo, filterString);
        }


        public bool AddCustomer(List<CustomerDetails> cust,string loggedInUserRoleId, string userId)
        {
          
            return dal.AddCustomer(cust,  loggedInUserRoleId,  userId);
        }

        public bool UpdateCustomer(List<CustomerDetails> cust, string loggedInUserRoleId, string userId)
        {
            return dal.UpdateCustomers(cust, loggedInUserRoleId,  userId);
        }

        public bool DeleteCustomer(int cust)
        {
            return dal.DeleteCustomers(cust);
        }


        public List<CustomerDetails> GetCustomerDetails(int custID)
        {
            List<CustomerDetails> list = dal.GetCustomerDetails(custID);

            //foreach(CustomerDetails cD in list)
            //{
            //    DateTime date = new DateTime(cD.DOB.Year, cD.DOB.Month, cD.DOB.Day);
            //    cD.DOB =
            //        Convert.ToDateTime(DateTime.ParseExact(Convert.ToString(cD.DOB), "dd-MM-yyyy", CultureInfo.InvariantCulture));
            //}

            return list;
        }

        public int FindCustomer(string userName,string DOB)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(DOB))
            {
                return -1;
            }
            else
            {
                DateTime dob = Convert.ToDateTime(DateTime.ParseExact(Convert.ToString(DOB), "dd-MM-yyyy", CultureInfo.InvariantCulture));
                return dal.FindCustomer(userName, dob);
            }
          
        }



    }
}
