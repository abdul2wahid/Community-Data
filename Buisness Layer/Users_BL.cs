using DAL.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BuisnessLayer
{
    public class Users_BL
    {
        Users_DAL dal;
        TokenManager.Token tokenManager;

        public Users_BL()
        {
            dal = new Users_DAL();
        }

        public List<UserModel> GetUsers(string loggedInUserRoleId, string userId, string sortOrder, int currentPageNo, string filterString, out int count)
        {
            List<UserModel> result = dal.GetUsers();
            count = result.Count;
            string[] filterStringArray = filterString.Split(';');
            if (Convert.ToInt32(loggedInUserRoleId) != 0) //except super user
            {
                UserModel loggedInUser = result.Find(x => x.UserId == Convert.ToInt32(userId));
                result.RemoveAll(x => x.RoleId <= Convert.ToInt32(loggedInUserRoleId));
                result.Insert(result.Count, loggedInUser);
                count = result.Count;
                if (!string.IsNullOrEmpty(filterString))
                {
                    if (!string.IsNullOrEmpty(filterStringArray[0]))
                    {
                        result = result.FindAll(x => x.Role == filterStringArray[0]);
                    }
                    
                }
                result = result.Skip((currentPageNo - 1) * Constants.Max_No_Rows).Take(Constants.Max_No_Rows).ToList();
            }
          
            return result;
        }


        public string AddUser(UserModel user, string loggedInUserRoleId, string userId)
        {
            string s = "Error,couldn't add user";
            if(dal.VerifyIfCustomerExist(user))
            {
                if(!dal.VerifyIfUserAlreadyAssignedRole(user))
                {
                    dal.GetUsers();
                    if (Convert.ToInt32(loggedInUserRoleId) != 0) //except super user
                    {
                        if (Convert.ToInt32(loggedInUserRoleId) <= user.RoleId)
                        {
                            if (dal.AddUser(user, userId))
                            {
                                s = "success";
                            }
                            
                        }
                        else
                        {
                            s = "Incorrect Previelage to add user";
                        }
                    }
                    else //If super user
                    {
                        if (dal.AddUser(user, userId))
                        {
                            s = "success";
                        }
                    }

                    return s;
                }
                else
                {
                    s = "User has role assigned to him, plase edit to change role";
                }

            }
            else
            {
                s = "customer doesn't exist";
            }

                return s;
        }


        public string Login(string username, string password, DateTime dt)
        {
            dynamic obj = dal.Login(username, password, dt);
            if (obj != null)
            {
                tokenManager = new TokenManager.Token();

                ClaimsIdentity claims = new ClaimsIdentity();
                claims.AddClaim(new Claim("Name", obj.Name));
                claims.AddClaim(new Claim("Role", obj.Role));
                claims.AddClaim(new Claim("Id", Convert.ToString(obj.Id)));
                claims.AddClaim(new Claim("RId", Convert.ToString(obj.RoleId)));
                return tokenManager.GenerateToken(claims);
            }
            return string.Empty;

        }


        /// <summary>
        ///  only super user and admin can rolled change role , contaccess via access policy set in controller
        /// </summary>
        /// <param name="user"></param>
        /// <param name="loggedInUserRoleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserModel UpdateUser(UserModel user, string loggedInUserRoleId, string userId)
        {
           
            if (Convert.ToInt32(loggedInUserRoleId) != 0)
            {
                if (Convert.ToInt32(loggedInUserRoleId) >= user.RoleId)
                {
                    user.UserId = -2;
                    return user;
                }
            }
            return dal.UpdateUser(user);
        }


        public List<UserModel> SearchUsers(UserModel search, string loggedInUserRoleId, string userId)
        {
           
            return dal.SearchUsers(search);
        }

            /// <summary>
            ///  passwor update
            /// </summary>
            /// <param name="user"></param>
            /// <param name="loggedInUserRoleId"></param>
            /// <param name="userId"></param>
            /// <returns></returns>
            public UpdatePasswordModel UpdatePassword(UpdatePasswordModel user, string loggedInUserRoleId, string userId)
        {

            if (Convert.ToInt32(userId) == user.UserId)
            {
                if (user.newPassword.Equals(user.confirmPassword))
                {

                    return dal.UpdatePassword(user,false); 
                }
            }
            else //reset for some one else
            {
                if(user.confirmPassword==string.Empty && user.newPassword==string.Empty
                    && user.oldPassword==string.Empty)
                {
                    return dal.UpdatePassword(user,true);
                }
            }
            return null;
        }

        


        public string DeleteUsers(int userIdToDelete,string loggedInUserRoleId, string userId)
        {
            string s = "Error couln't delete user";

            if (Convert.ToInt32(loggedInUserRoleId) != 0) //except super user
            {
                if (Convert.ToInt32(loggedInUserRoleId) < userIdToDelete)
                {

                    if (dal.DeleteUsers(new Users()
                    {
                        UserId = userIdToDelete
                    }))
                    {
                        s = "SUCCESS";
                    }


                }
                else
                {
                    s = "Incorrect previelges, couldn't delete user";
                }
            }
            else
            {
                if (dal.DeleteUsers(new Users()
                {
                    UserId = userIdToDelete
                }))
                {
                    s = "SUCCESS";
                }
            }

            return s;
        }
    }
}
