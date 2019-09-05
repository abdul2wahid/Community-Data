using DAL;
using DAL.Entities;
using Models;
using System;
using System.Collections.Generic;

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
            return dal.GetCustomerDetails(custID);
        }


    }
}
