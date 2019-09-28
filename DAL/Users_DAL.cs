using Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DAL.Entities
{
    public class Users_DAL
    {

        public bool AddUser(UserModel user,string createdPersonUserId)
        {
            using (var context = new connected_usersContext())
            {
                try
                {
                    int custID = context.Customers.Where(x => x.Name == user.UserName
            && x.Dob.Day.ToString("D2") + "/" + x.Dob.Month.ToString("D2") + "/" + x.Dob.Year == user.DOB).Select(x=>x.CustomerId).FirstOrDefault();

                    context.Users.Add(new Users()
                    {
                        UserId = custID,
                        Password = "12345",
                        RoleId = user.RoleId,
                        CreatedBy = Convert.ToInt32(createdPersonUserId),
                        CreatedTime = DateTime.Now,
                    });


                    context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }




        public bool VerifyIfCustomerExist(UserModel user)
        {
            using (var context = new connected_usersContext())
            {

                Customers exist = context.Customers.Where(x => x.Name == user.UserName
             && x.Dob.Day.ToString("D2") + "/" + x.Dob.Month.ToString("D2") + "/" + x.Dob.Year == user.DOB).FirstOrDefault();

                if (exist == null  || exist.CustomerId<=-1)
                {
                    return false;
                }

                return true;
            }
        }

        public bool VerifyIfUserAlreadyAssignedRole(UserModel user)
        {
            using (var context = new connected_usersContext())
            {
                bool userAlreadyExist = false;

                var exist = context.Customers.Where(x => x.Name == user.UserName
              && x.Dob.Day.ToString("D2") + "/" + x.Dob.Month.ToString("D2") + "/" + x.Dob.Year == user.DOB).FirstOrDefault(); ;
                
                if (exist != null && exist.CustomerId > -1)
                {
                    Users userExist = context.Users.Find(exist.CustomerId);
                    if(userExist!=null)
                    {
                        userAlreadyExist = true;
                    }
                    
                }
              
                return userAlreadyExist;
            }
        }


        public dynamic Login(string username, string password, DateTime dt)
        {
            using (var context = new connected_usersContext())
            {

              Customers cust = context.Customers.Where(x=>x.Name==username  && x.Dob==dt).FirstOrDefault();
                if (cust != null)
                {
                    Users user = context.Users.Where(x => x.Password == password).FirstOrDefault();
                    if (user != null)
                    {
                        Roles roles = context.Roles.Where(x => x.RoleId == user.RoleId).FirstOrDefault();
                        if (roles != null)
                        {
                            dynamic obj = new ExpandoObject();
                            obj.Name = cust.Name;
                            obj.Role = roles.RoleName;
                            obj.Id = cust.CustomerId;
                            obj.RoleId = roles.RoleId;
                            return obj;
                        }
                    }
                }

                return null;

               
            }

        }


        public List<UserModel> GetUsers()
        {

            using (var context = new connected_usersContext())
            {

                List<UserModel> users = context.Users
                .Join(context.Customers, u => u.UserId, c => c.CustomerId, (u, c) => new { u, c })
                .Join(context.Roles, n => n.u.RoleId, ro => ro.RoleId, (n, ro) => new { n, ro })
                .Select(m => new UserModel()
                {
                    UserName = m.n.c.Name,
                    DOB = Convert.ToString(m.n.c.Dob),
                    UserId = m.n.c.CustomerId,
                    CreatedBy = Convert.ToString(m.n.c.CreatedBy),
                    Role = m.n.u.Role.RoleName,
                    RoleId=m.n.u.Role.RoleId,
                }).ToList();

              // List<string> CreatedByIds = users.Select(o => o.CreatedBy).ToList();
                var createdByNameList = context.Customers
                   .Select(m => new
                   {
                       CreatedByString = m.Name,
                       CreatedById = m.CustomerId,
                   }).Distinct();


                foreach (UserModel umodel in users)
                {
                    umodel.CreatedBy = createdByNameList.Where(x => x.CreatedById.ToString() == umodel.CreatedBy).Select(y => y.CreatedByString).FirstOrDefault();
                }



                //IQueryable<UserModel> usersUpdated = users
                //    .Join(createdByNameList, u => u.CreatedBy, c => c.CreatedById.ToString(), (u, c) => new { u, c })
                //    .Select(m => new UserModel()
                //    {
                //        UserName = m.u.UserName,
                //        DOB = m.u.DOB,
                //        UserId = m.u.UserId,
                //        CreatedBy = m.c.CreatedByString,
                //        Role = m.u.Role,

                //    });


                return users;
            }
        }


        public List<UserModel> SearchUsers(UserModel search)
        {
            using (var context = new connected_usersContext())
            {

                List<UserModel> users = context.Customers.Where(x => x.Name.Equals(search.UserName)).Select(m => new UserModel()
                {
                    UserName = m.Name,
                    DOB = Convert.ToString(m.Dob),
                    UserId = m.CustomerId,
                }).ToList();

                return users;
            }
        }


        public UserModel UpdateUser(UserModel user)
        {
            using (var context = new connected_usersContext())
            {

                var entity = context.Users.Find(user.UserId);
                if (entity == null)
                {
                    return null;
                }

               var verify= context.Customers.Find(user.UserId);
                if (verify.Name == user.UserName && verify.CustomerId == user.UserId && Convert.ToString(verify.Dob) == user.DOB)
                {
                    entity.RoleId = context.Roles.Where(x => x.RoleName == user.Role).Select(y => y.RoleId).FirstOrDefault();
                    context.SaveChanges();
                }
                else
                    return null;
                //context.Customers.Update(cust);
                //context.SaveChanges();

                return user;
            }
        }


        public UpdatePasswordModel UpdatePassword(UpdatePasswordModel user, bool isReset)
        {
            using (var context = new connected_usersContext())
            {

                var entity = context.Users.Find(user.UserId);
                if (entity == null)
                {
                    return null;
                }

                var verify = context.Customers.Find(user.UserId);
                if (verify.Name == user.UserName && verify.CustomerId == user.UserId && Convert.ToString(verify.Dob) == user.DOB)
                {
                    if (isReset)
                    {
                        entity.Password = "12345";
                        user.newPassword = "12345";
                    }
                    else
                    {
                        entity.Password = user.newPassword;
                    }
                    context.SaveChanges();
                }
                else
                    return null;
                //context.Customers.Update(cust);
                //context.SaveChanges();
                
                return user;
            }
        }

        public bool DeleteUsers(Users user)
        {

            using (var context = new connected_usersContext())
            {
                var entity = context.Users.Find(user.UserId);
                if (entity == null)
                {
                    return false;
                }


                context.Users.Remove(user);
                context.SaveChanges();
                return true;
            }
        }

        
    }
}
