using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Globalization;

namespace DAL
{
    
    public class Customer_DAL
    {


        public bool AddCustomer(CustomerDetails cust, string loggedInUserRoleId, string userId)
        {
            bool result = true;
            CustomerDetails c = cust;

                using (var context = new connected_usersContext())
                {

                  
                    using (IDbContextTransaction trans = context.Database.BeginTransaction())
                    {
                        try
                        {

                        var customer = context.Customers.Add(new Customers()
                        {
                            Name = c.Name,
                            Dob = DateTime.Parse(c.DOB),
                                ArabicEducationId = c.arabicEducationID,
                                EducationId = c.educationId,
                                OccupationId = c.OccupationId,
                                MaritalStatusId = c.MaritalStatusId ?? 0,
                                GenderId = c.GenderId ?? 0,
                                MobileNumber = c.MobileNumber,
                                OccupdationDetails = c.OccupationDetails,
                                EducationDetail = c.EducationDetails,
                                CreatedBy = Convert.ToInt32(userId),
                                CreatedTime = DateTime.Now,
                                UpdatedBy = null,
                                UpdatedTime = null,


                            });
                            context.SaveChanges();

                      

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
                            context.SaveChanges();

                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            result = false;
                        }
                        trans.Commit();
                    }
                }
            

            return result;
        }

      

        public List<CustomerModel> GetCustomers(string sortOrder, int currentPageNo, string filterString, int pageSize,out int count)
        {
            List<CustomerModel> list = new List<CustomerModel>();
            try
            {
                using (var context = new connected_usersContext())
                {

                    list = context.Customers
                                       .Join(context.Gender, x => x.GenderId, y => y.GenderId, (x, y) => new { x, y })
                                        .Join(context.Maritalstatus, a => a.x.MaritalStatusId, b => b.MaritalStatusId, (a, b) => new { a, b })
                                         .Join(context.Occupation, i => i.a.x.OccupationId, j => j.OccupationId, (i, j) => new { i, j })
                                       .Select(
                                       p => new CustomerModel()
                                       {
                                           CustomerID = p.i.a.x.CustomerId,
                                           Name = p.i.a.x.Name,
                                           Gender = p.i.a.y.Gender1,
                                           Age = DateTime.Now.Year - p.i.a.x.Dob.Year,
                                           MaritalStatus = p.i.b.MaritalStatus1,
                                           Occupation = p.j.OccuptionName,
                                           MobileNumber = p.i.a.x.MobileNumber
                                       }).ToList();

                    count = list.Count;

                    if (!string.IsNullOrEmpty(filterString))
                    {
                        string[] filterStringArray = filterString.Split(';');
                        if (filterStringArray.Length >= 4 && !(string.IsNullOrEmpty(filterStringArray[3])))
                        {
                            string searchString = filterStringArray[3].Trim();
                            list = list.Where(x => x.Name.Contains(searchString)).ToList();

                        }
                        else
                        {
                            if (filterStringArray.Length >= 1  && !(string.IsNullOrEmpty(filterStringArray[0])))
                            {
                                list = list.Where(x => x.Gender == filterStringArray[0].Trim()).ToList();
                            }
                            if (filterStringArray.Length >= 2 && !(string.IsNullOrEmpty(filterStringArray[1])))

                            {
                                list = list.Where(x => x.MaritalStatus == filterStringArray[1]).ToList();
                            }

                            if (filterStringArray.Length >= 3 && !(string.IsNullOrEmpty(filterStringArray[2])))
                            {
                                list = list.Where(x => x.Occupation == filterStringArray[2]).ToList();
                            }
                        }
                        count = list.Count;
                        
                    }

                  
                        list = list.Skip((currentPageNo - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch(Exception ex)
            {
                count = 0;
                //log  here
            }

            return list;
        }

        public List<CustomerDetails> GetCustomerDetails(int custID)
        {
            List<CustomerDetails> custList = new List<CustomerDetails>();
            
            using (var context = new connected_usersContext())
            {
                custList.Add(context.Customers.AsNoTracking().Include(x => x.CustomerAddress).Select(m => new CustomerDetails()
                {
                    CustomerID = m.CustomerId,
                    Name = m.Name,
                    Age = DateTime.Now.Year - m.Dob.Year,
                    DOB = m.Dob.Day.ToString("D2") + "-" + m.Dob.Month.ToString("D2") + "-"+m.Dob.Year,
                    Gender = m.Gender.Gender1,
                    MobileNumber = m.MobileNumber,
                    MaritalStatus = m.MaritalStatus.MaritalStatus1,
                    Occupation = m.Occupation.OccuptionName,


                    OccupationDetails = m.OccupdationDetails,
                    EducationDetails = m.EducationDetail,
                    educationName = m.Education.EducationName,
                    arabicEducationName = m.ArabicEducation.ArabicEducationName,

                    Address1 = m.CustomerAddress.Address1,
                    Address2 = m.CustomerAddress.Address2,
                    State = m.CustomerAddress.State.State,
                    City = m.CustomerAddress.City.City1,
                    Area = m.CustomerAddress.Area,
                    Pin = m.CustomerAddress.Pin.Pin,
                    DependantParentID=-1,


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
                        Name = m.Name,
                        Age = DateTime.Now.Year - m.Dob.Year,
                        DOB = m.Dob.Day.ToString("D2") + "-" + m.Dob.Month.ToString("D2") + "-" + m.Dob.Year,
                        Gender = m.Gender.Gender1,
                        MobileNumber = m.MobileNumber,
                        MaritalStatus = m.MaritalStatus.MaritalStatus1,
                        Occupation = m.Occupation.OccuptionName,


                        OccupationDetails = m.OccupdationDetails,
                        EducationDetails = m.EducationDetail,
                        educationName = m.Education.EducationName,
                        arabicEducationName = m.ArabicEducation.ArabicEducationName,

                        Address1 = m.CustomerAddress.Address1,
                        Address2 = m.CustomerAddress.Address2,
                        State = m.CustomerAddress.State.State,
                        City = m.CustomerAddress.City.City1,
                        Area = m.CustomerAddress.Area,
                        Pin = m.CustomerAddress.Pin.Pin,
                        DependantParentID = (int?)custID,

                    }).SingleOrDefault(x => x.CustomerID == id));
                }
            }

            return custList;
        }

        /// <summary>
        /// Return all users for excel download
        /// </summary>
        /// <returns></returns>
        public List<CustomerDetails> GetCustomerDetails()
        {
            List<CustomerDetails> custList = new List<CustomerDetails>();

            using (var context = new connected_usersContext())
            {
                custList=context.Customers.AsNoTracking().Include(x => x.CustomerAddress).Select(m => new CustomerDetails()
                {
                    CustomerID = m.CustomerId,
                    Name = m.Name,
                    Age = DateTime.Now.Year - m.Dob.Year,
                    DOB = m.Dob.Day.ToString("D2") + "/" + m.Dob.Month.ToString("D2") + "/" + m.Dob.Year,
                    Gender = m.Gender.Gender1,
                    MobileNumber = m.MobileNumber,
                    MaritalStatus = m.MaritalStatus.MaritalStatus1,
                    Occupation = m.Occupation.OccuptionName,


                    OccupationDetails = m.OccupdationDetails,
                    EducationDetails = m.EducationDetail,
                    educationName = m.Education.EducationName,
                    arabicEducationName = m.ArabicEducation.ArabicEducationName,

                    Address1 = m.CustomerAddress.Address1,
                    Address2 = m.CustomerAddress.Address2,
                    State = m.CustomerAddress.State.State,
                    City = m.CustomerAddress.City.City1,
                    Area = m.CustomerAddress.Area,
                    Pin = m.CustomerAddress.Pin.Pin,
                    DependantParentID = -1,


                }).ToList();
            }

            return custList;
        }

        public int FindCustomer(string userName, DateTime DOB)
        {
           
            using (var context = new connected_usersContext())
            {
                Customers cust  = context.Customers.Where(x => x.Name == userName && x.Dob == DOB).SingleOrDefault();

                if (cust != null)
                {
                    Reltionship rela = context.Reltionship.Where(x => x.ChildernId == cust.CustomerId || x.WifeId == cust.CustomerId).SingleOrDefault();
                    if (rela == null)
                        return cust.CustomerId;
                }
                return -1;

            }

        }
    


        public bool UpdateCustomers(List<CustomerDetails> cust, string loggedInUserRoleId, string userId)
        {


            using (var context = new connected_usersContext())
            {

                foreach (CustomerDetails c in cust)
                {

                    //If dependant user needs to be deleted
                    if (c.DependantToBeDeleted ?? false)
                    {
                        int? id = (int?)c.CustomerID;
                        Reltionship cusDe = context.Reltionship.Where(x => x.ChildernId == id || x.WifeId ==id).SingleOrDefault();
                        if (cusDe != null)
                        {
                            context.Reltionship.Remove(cusDe);
                            context.SaveChanges();
                        }
                        continue;
                    }
                    //else if (c.CustomerID == -1)  //Add new user
                    //{
                    //    var customer = context.Customers.Add(new Customers()
                    //    {
                    //        Name = c.CutomerName,
                    //        Dob = c.DOB,
                    //        ArabicEducationId = c.arabicEducationID,
                    //        EducationId = c.educationId,
                    //        OccupationId = c.OccupationId,
                    //        MaritalStatusId = c.MaritalStatusId ?? 0,
                    //        GenderId = c.GenderId ?? 0,
                    //        MobileNumber = c.MobileNumber,
                    //        OccupdationDetails = c.OccupationDetails ?? string.Empty,
                    //        EducationDetail = c.EducationDetails ?? string.Empty,
                    //        CreatedBy = Convert.ToInt32(userId),
                    //        CreatedTime = DateTime.Now,
                    //        UpdatedBy = null,
                    //        UpdatedTime = null,


                    //    });
                    //    context.SaveChanges();

                    //    context.CustomerAddress.Add(new CustomerAddress()
                    //    {
                    //        CustomerId = customer.Entity.CustomerId,
                    //        Address1=c.Address1,
                    //        Address2=c.Address2,
                    //        Area=c.Area,
                    //        StateId=c.StateId,
                    //        CityId=c.CityId,
                    //        PinId=c.PinId,
                    //    });
                    //    context.SaveChanges();

                    //    if (c.DependantToBeAddedAsChild ?? false)
                    //    {
                    //        context.Reltionship.Add(new Reltionship()
                    //        {
                    //            UserId = c.DependantParentID ?? 0,
                    //            ChildernId = customer.Entity.CustomerId,
                    //        });
                    //    }
                    //    else
                    //    {
                    //        context.Reltionship.Add(new Reltionship()
                    //        {
                    //            UserId = c.DependantParentID??0,
                    //            WifeId = customer.Entity.CustomerId,
                    //        });
                    //    }
                    //    context.SaveChanges();
                    //}

                    else
                    {

                        Customers custom = context.Customers.Where(x => x.CustomerId == c.CustomerID).Include(x => x.CustomerAddress).SingleOrDefault();
                        custom.ArabicEducationId = c.arabicEducationID ?? 0;
                        custom.EducationId = c.educationId ?? 0;
                        custom.OccupationId = c.OccupationId ?? 0;
                        custom.MaritalStatusId = c.MaritalStatusId ?? 0;
                        custom.GenderId = c.GenderId ?? 0;
                        if (!string.IsNullOrEmpty(c.MobileNumber))
                            custom.MobileNumber = c.MobileNumber;
                        if (!string.IsNullOrEmpty(c.OccupationDetails))
                            custom.OccupdationDetails = c.OccupationDetails;
                        if (!string.IsNullOrEmpty(c.EducationDetails))
                            custom.EducationDetail = c.EducationDetails;
                        custom.UpdatedBy = Convert.ToInt32(userId);
                        custom.UpdatedTime = DateTime.Now;
                        if (custom.CustomerAddress != null)
                        {
                            if (!string.IsNullOrEmpty(c.Address1))
                                custom.CustomerAddress.Address1 = c.Address1;
                            if (!string.IsNullOrEmpty(c.Address2))
                                custom.CustomerAddress.Address2 = c.Address2;
                            if (!string.IsNullOrEmpty(c.Area))
                                custom.CustomerAddress.Area = c.Area;
                            custom.CustomerAddress.CityId = c.CityId;
                            custom.CustomerAddress.StateId = c.StateId;
                            custom.CustomerAddress.PinId = c.PinId;
                        }

                        context.SaveChanges();

                        if (c.DependantParentID > 0)  //Add existing customer as dependent
                        {
                            Reltionship re = context.Reltionship.Where(x=>x.ChildernId == c.CustomerID
                            ||x.WifeId ==c.CustomerID || x.UserId==c.CustomerID).SingleOrDefault();

                            if (re == null)
                            {
                                if (c.DependantToBeAddedAsChild ?? false) //add dependent as child
                                {
                                    context.Reltionship.Add(new Reltionship()
                                    {
                                        UserId = c.DependantParentID ?? 0,
                                        ChildernId = c.CustomerID,
                                    });
                                }
                                else //add dependent as wife
                                {
                                    context.Reltionship.Add(new Reltionship()
                                    {
                                        UserId = c.DependantParentID ?? 0,
                                        WifeId = c.CustomerID,
                                    });
                                }
                            }
                        }
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

                //If Parent , ensure dependent childrens are not alive
                List<Reltionship> hasDependent = context.Reltionship.Where(x => x.UserId == cust).ToList(); ;
                if (hasDependent.Count > 0)
                {
                    return false;
                }

                //If children, Remove relationShip
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


                //Remove Address
                List<Users> usersList = context.Users.Where(x => x.UserId == cust).ToList(); ;

                foreach (Users user in usersList)
                {
                    context.Users.Remove(user);
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
