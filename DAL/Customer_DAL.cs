﻿using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace DAL
{
    
    public class Customer_DAL
    {


        public bool AddCustomer(List<CustomerDetails> cust, string loggedInUserRoleId, string userId)
        {
            bool result = true;

            int dependentId = -1;

            foreach (CustomerDetails c in cust)
            {

                using (var context = new connected_usersContext())
                {

                    //If already existing customer and user tryign to add new dependant customer
                    if (c.CustomerID > -1)
                    {
                        Customers cu = context.Customers.Find(c.CustomerID);
                        if (cu != null && cu.Name == c.CutomerName && cu.Dob == c.DOB)
                        {
                            dependentId = c.CustomerID;
                            continue;  //just take customer ID and return to add next user
                        }
                        else
                        {
                            return false;
                        }
                    }

                    using (IDbContextTransaction trans = context.Database.BeginTransaction())
                    {
                        try
                        {

                            var customer = context.Customers.Add(new Customers()
                            {
                                Name = c.CutomerName,
                                Dob = c.DOB,
                                ArabicEducationId = c.arabicEducatioID,
                                EducationId = c.educationId,
                                OccupationId = c.OccupationId,
                                MaritalStatusId = c.MaritalStatusId,
                                GenderId = c.GenderId,
                                MobileNumber = c.MobileNumber,
                                OccupdationDetails = c.OccupationDetails,
                                EducationDetail = c.EducationDetails,
                                CreatedBy = Convert.ToInt32(userId),
                                CreatedTime = DateTime.Now,
                                UpdatedBy = null,
                                UpdatedTime = null,


                            });
                            context.SaveChanges();

                            if (c.WifeId == -1 && c.ChildernId == -1 && dependentId == -1)
                                dependentId = customer.Entity.CustomerId;

                            if (c.WifeId != -1 || c.ChildernId != -1)
                            {
                                Reltionship rela = null;

                                if (c.WifeId != -1)
                                {
                                    rela = new Reltionship()
                                    {
                                        UserId = dependentId,
                                        WifeId = customer.Entity.CustomerId,
                                    };
                                }
                                else if (c.ChildernId != -1)
                                {
                                    rela = new Reltionship()
                                    {
                                        UserId = dependentId,
                                        ChildernId = customer.Entity.CustomerId,
                                    };
                                }

                                context.Reltionship.Add(rela);
                                context.SaveChanges();
                            }

                            context.CustomerAddress.Add(new CustomerAddress()
                            {
                                CustomerId = customer.Entity.CustomerId,
                                Address1 = c.Address1,
                                Address2 = c.Address2,
                                Area = c.Area,
                                StateId = c.StateId,
                                CityId = c.CityId,
                                PinId = c.PinId,

                            });


                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            result = false;
                        }
                        trans.Commit();
                    }
                }
            }

            return result;
        }

        public List<CustomerModel> GetCustomers(string sortOrder, int currentPageNo, string filterString)
        {


            using (var context = new connected_usersContext())
            {
                if (filterString != string.Empty)
                {
                    string[] filterStringArray = filterString.Split(';');
                    List<CustomerModel> s = context.Customers
                                        .Join(context.Gender, x => x.GenderId, y => y.GenderId, (x, y) => new { x, y })
                                         .Join(context.Maritalstatus, a => a.x.MaritalStatusId, b => b.MaritalStatusId, (a, b) => new { a, b })
                                          .Join(context.Occupation, i => i.a.x.OccupationId, j => j.OccupationId, (i, j) => new { i, j })
                                        .Select(
                                        p => new CustomerModel()
                                        {
                                            CustomerID = p.i.a.x.CustomerId,
                                            CutomerName = p.i.a.x.Name,
                                            Gender = p.i.a.y.Gender1,
                                            Age = DateTime.Now.Year - p.i.a.x.Dob.Year,
                                            MaritalStatus = p.i.b.MaritalStatus1,
                                            Occupation = p.j.OccuptionName,
                                            MobileNumber = p.i.a.x.MobileNumber
                                        }).ToList();

                    if (string.IsNullOrEmpty(filterStringArray[0]))
                    {
                        s = s.FindAll(x => x.Gender == filterStringArray[0]);
                    }
                    if (string.IsNullOrEmpty(filterStringArray[1]))
                    {
                        s.FindAll(x => x.MaritalStatus == filterStringArray[1]);
                    }
                    if (string.IsNullOrEmpty(filterStringArray[2]))
                    {
                        s.FindAll(x => x.Occupation == filterStringArray[2]);
                    }

                    s = s.OrderBy(x => x.CustomerID).Skip((currentPageNo - 1) * Constants.Max_No_Rows).Take(Constants.Max_No_Rows).ToList();
                    return s;
                }
                else if(currentPageNo>=0)
                {

                    return context.Customers
                        .Join(context.Gender, x => x.GenderId, y => y.GenderId, (x, y) => new { x, y })
                         .Join(context.Maritalstatus, a => a.x.MaritalStatusId, b => b.MaritalStatusId, (a, b) => new { a, b })
                          .Join(context.Occupation, i => i.a.x.OccupationId, j => j.OccupationId, (i, j) => new { i, j })
                        .Select(
                        p => new CustomerModel()
                        {
                            CustomerID = p.i.a.x.CustomerId,
                            CutomerName = p.i.a.x.Name,
                            Gender = p.i.a.y.Gender1,
                            Age = DateTime.Now.Year - p.i.a.x.Dob.Year,
                            MaritalStatus = p.i.b.MaritalStatus1,
                            Occupation = p.j.OccuptionName,
                            MobileNumber = p.i.a.x.MobileNumber
                        }).OrderBy(x => x.CustomerID).Skip((currentPageNo - 1) * Constants.Max_No_Rows).Take(Constants.Max_No_Rows).ToList();
                }
                else
                {
                    return context.Customers
                      .Join(context.Gender, x => x.GenderId, y => y.GenderId, (x, y) => new { x, y })
                       .Join(context.Maritalstatus, a => a.x.MaritalStatusId, b => b.MaritalStatusId, (a, b) => new { a, b })
                        .Join(context.Occupation, i => i.a.x.OccupationId, j => j.OccupationId, (i, j) => new { i, j })
                      .Select(
                      p => new CustomerModel()
                      {
                          CustomerID = p.i.a.x.CustomerId,
                          CutomerName = p.i.a.x.Name,
                          Gender = p.i.a.y.Gender1,
                          Age = DateTime.Now.Year - p.i.a.x.Dob.Year,
                          MaritalStatus = p.i.b.MaritalStatus1,
                          Occupation = p.j.OccuptionName,
                          MobileNumber = p.i.a.x.MobileNumber
                      }).ToList();
                }
            }
        }

        public List<CustomerDetails> GetCustomerDetails(int custID)
        {
            List<CustomerDetails> custList = new List<CustomerDetails>();

            using (var context = new connected_usersContext())
            {
                custList.Add(context.Customers.AsNoTracking().Include(x => x.CustomerAddress).Select(m => new CustomerDetails()
                {
                    CustomerID = m.CustomerId,
                    CutomerName = m.Name,
                    Age =m.Dob.Year-DateTime.Now.Year,
                    Gender = m.Gender.Gender1,
                    MobileNumber = m.MobileNumber,
                    MaritalStatus = m.MaritalStatus.MaritalStatus1,

                    Address1 = m.CustomerAddress.Address1,
                    Address2 = m.CustomerAddress.Address2,
                    StateId = m.CustomerAddress.StateId,
                    CityId = m.CustomerAddress.CityId,
                    Area = m.CustomerAddress.Area,
                    PinId = m.CustomerAddress.PinId,


                }).SingleOrDefault(x => x.CustomerID == custID));

                List<Reltionship> dependants = context.Reltionship.Where(x => x.UserId == custID).ToList();

                foreach(Reltionship dep in dependants)
                {

                    int? id = -1;
                    if (dep.WifeId != null)
                        id = dep.WifeId;
                    else if (dep.ChildernId != null)
                        id = dep.ChildernId;


                    custList.Add(context.Customers.AsNoTracking().Include(x => x.CustomerAddress).Select(m => new CustomerDetails()
                    {
                        CustomerID = m.CustomerId,
                        CutomerName = m.Name,
                        Age = m.Dob.Year - DateTime.Now.Year,
                        Gender = m.Gender.Gender1,
                        MobileNumber = m.MobileNumber,
                        MaritalStatus = m.MaritalStatus.MaritalStatus1,

                        Address1 = m.CustomerAddress.Address1,
                        Address2 = m.CustomerAddress.Address2,
                        StateId = m.CustomerAddress.StateId,
                        CityId = m.CustomerAddress.CityId,
                        Area = m.CustomerAddress.Area,
                        PinId = m.CustomerAddress.PinId,


                    }).SingleOrDefault(x => x.CustomerID == id));
                }
            }

            return custList;
        }


        public bool UpdateCustomers(List<CustomerDetails> cust, string loggedInUserRoleId, string userId)
        {

        
            using (var context = new connected_usersContext())
            {

                foreach (CustomerDetails c in cust)
                {

                    //If dependant user needs to be deleted
                    if (c.DependantToBeDeleted)
                    {
                        Reltionship cusDe = context.Reltionship.Find(c.CustomerID);
                        context.Reltionship.Remove(cusDe);
                        context.SaveChanges();
                        continue;
                    }
                    else if (c.CustomerID == -1)  //Add new user
                    {
                        var customer = context.Customers.Add(new Customers()
                        {
                            Name = c.CutomerName,
                            Dob = c.DOB,
                            ArabicEducationId = c.arabicEducatioID,
                            EducationId = c.educationId,
                            OccupationId = c.OccupationId,
                            MaritalStatusId = c.MaritalStatusId,
                            GenderId = c.GenderId,
                            MobileNumber = c.MobileNumber,
                            OccupdationDetails = c.OccupationDetails,
                            EducationDetail = c.EducationDetails,
                            CreatedBy = Convert.ToInt32(userId),
                            CreatedTime = DateTime.Now,
                            UpdatedBy = null,
                            UpdatedTime = null,


                        });
                        context.SaveChanges();

                        if(c.DependantToBeAddedAsChild)
                        {
                            context.Reltionship.Add(new Reltionship()
                            {
                                UserId = c.DependantParentID,
                                ChildernId = customer.Entity.CustomerId,
                            });
                        }
                        else
                        {
                            context.Reltionship.Add(new Reltionship()
                            {
                                UserId = c.DependantParentID,
                                WifeId = customer.Entity.CustomerId,
                            });
                        }
                        context.SaveChanges();
                    }
                    else
                    {

                        Customers custom = context.Customers.Where(x => x.CustomerId == c.CustomerID).SingleOrDefault();
                        custom.ArabicEducationId = c.arabicEducatioID;
                        custom.EducationId = c.educationId;
                        custom.OccupationId = c.OccupationId;
                        custom.MaritalStatusId = c.MaritalStatusId;
                        custom.GenderId = c.GenderId;
                        custom.MobileNumber = c.MobileNumber;
                        custom.OccupdationDetails = c.OccupationDetails;
                        custom.EducationDetail = c.EducationDetails;
                        custom.UpdatedBy = Convert.ToInt32(userId);
                        custom.UpdatedTime = DateTime.Now;
                        custom.CustomerAddress.Address1 = c.Address1;
                        custom.CustomerAddress.Address2 = c.Address1;
                        custom.CustomerAddress.Area = c.Area;
                        custom.CustomerAddress.CityId = c.CityId;
                        custom.CustomerAddress.StateId = c.StateId;
                        custom.CustomerAddress.PinId = c.PinId;

                        context.SaveChanges();
                    }
                }

                

               
            }
            return true;
           

        }


        public bool DeleteCustomers(int cust)
        {
           
            using (var context = new connected_usersContext())
            {
                var entity = context.Customers.Find(cust);
                if (entity == null)
                {
                    return false;
                }

                //Remove relationShip
                List<Reltionship> record = context.Reltionship.Where(x => x.ChildernId == cust || x.WifeId == cust).ToList(); ;

                foreach (Reltionship rela in record)
                {
                    context.Reltionship.Remove(rela);
                }
                context.SaveChanges();


                //Remove Address
                List<CustomerAddress> customeAddressList = context.CustomerAddress.Where(x => x.CustomerId == cust).ToList(); ;

                foreach (CustomerAddress rela in customeAddressList)
                {
                    context.CustomerAddress.Remove(rela);
                }
                context.SaveChanges();


                //Remove Customer
                Customers customer = context.Customers.Where(x => x.CustomerId == cust).SingleOrDefault();
                context.Customers.Remove(customer);
                context.SaveChanges();

                return true;
            }
        }
    }
}