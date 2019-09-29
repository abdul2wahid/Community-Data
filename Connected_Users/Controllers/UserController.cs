using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BuisnessLayer;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;

namespace Connected_Users.Controllers
{
    [Authorize(Policy = "AllLoggedInUsersPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        Users_BL usersBL;

        public UserController()
        {
            usersBL = new Users_BL();
        }


        // GET: api/User
        [HttpGet]
        public IActionResult Get(string sortOrder, int currentPageNo, string filterString)
        {
            int count = 0;
            List<UserModel> list =usersBL.GetUsers(HttpContext.User.FindFirst(c => c.Type == "RId").Value,
               HttpContext.User.FindFirst(c => c.Type == "Id").Value, sortOrder, currentPageNo, filterString,out count);


            if (list != null)
            {
                var obj = new
                {
                    Count = count,
                    pageIndex = currentPageNo,
                    pageSize = Startup.PageSize,
                    items = list,
                };
                return (Ok(obj));
            }
            else
            {
                var obj = new
                {
                    Count = count,
                    pageIndex = currentPageNo,
                    pageSize = Startup.PageSize,
                    items = new List<CustomerModel>(),
                };
                return (Ok(obj));
            }
            
        }

        [Authorize(Policy = "SU&AdminPolicy")]
        // GET: api/User
        [HttpGet, Route("~/api/[controller]/SearchUser")]
        public List<UserModel> Get(UserModel search)
        {
            return usersBL.SearchUsers(search,HttpContext.User.FindFirst(c => c.Type == "RId").Value,
                HttpContext.User.FindFirst(c => c.Type == "Id").Value);

        }

        //// GET: api/User/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: Login
        [AllowAnonymous]
        [HttpPost, Route("~/api/[controller]/Login")]
        public IActionResult Post([FromBody] dynamic val)
        {
            string token = usersBL.Login(Convert.ToString(val.username), Convert.ToString(val.password), Convert.ToDateTime(DateTime.ParseExact(Convert.ToString(val.dob), "dd-MM-yyyy", CultureInfo.InvariantCulture)));
            if (String.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost, Route("~/api/[controller]/Logout")]
        public IActionResult Post()
        {
          
            return Ok(true);
        }


        [Authorize(Policy = "SU&AdminPolicy")]
        [HttpPost]
        public IActionResult Post([FromBody] UserModel value)
        {
            string str = usersBL.AddUser(value, HttpContext.User.FindFirst(c => c.Type == "RId").Value,
                HttpContext.User.FindFirst(c => c.Type == "Id").Value);
            return Ok(JsonConvert.SerializeObject(str));
        }


        [Authorize(Policy = "SU&AdminPolicy")]
        // PUT: api/User/5
        [HttpPut("{id}"), Route("~/api/[controller]/UpdateRole")]
        public IActionResult Put( [FromBody] UserModel value)
        {

            UserModel user = usersBL.UpdateUser(value,HttpContext.User.FindFirst(c => c.Type == "RId").Value,
                HttpContext.User.FindFirst(c => c.Type == "Id").Value);
            if (user!=null && user.UserId==-2)
            {
                return Forbid();
            }


             return Ok(user);
        }

        [Authorize(Policy = "SU&AdminPolicy")]
        [HttpPut("{id}"), Route("~/api/[controller]/UpdatePassword")]
        public IActionResult Put([FromBody] UpdatePasswordModel value)
        {

            UserModel user = usersBL.UpdatePassword(value, HttpContext.User.FindFirst(c => c.Type == "RId").Value,
                HttpContext.User.FindFirst(c => c.Type == "Id").Value);
            if (user != null )
            {
                return BadRequest();
            }


            return Ok(user);
        }

        // DELETE: api/ApiWithActions/5
        [Authorize(Policy = "SU&AdminPolicy")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            string str = usersBL.DeleteUsers(id, HttpContext.User.FindFirst(c => c.Type == "RId").Value,
                HttpContext.User.FindFirst(c => c.Type == "Id").Value);
            return Ok(JsonConvert.SerializeObject(str));
        }
    }
}
