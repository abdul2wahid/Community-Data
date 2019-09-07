using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Connected_Users.Controllers
{
    [Authorize(Policy = "AllLoggedInUsersPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {


        Utility_BL utilityBL;

        public UtilityController()
        {
            utilityBL = new Utility_BL();
        }

        // GET: api/Utility
        [HttpGet, Route("~/api/[controller]/GetRoles")]
        public IActionResult Get()
        {

            return Ok(utilityBL.GetRoles(
            HttpContext.User.FindFirst(c => c.Type == "RId").Value,
                HttpContext.User.FindFirst(c => c.Type == "Id").Value));
        }

        // GET: api/Utility/5
        [HttpGet, Route("~/api/[controller]/GetGender")]
        public IActionResult GetGender()
        {
            return Ok(utilityBL.GetGender(
          HttpContext.User.FindFirst(c => c.Type == "RId").Value,
              HttpContext.User.FindFirst(c => c.Type == "Id").Value));
        }


        [HttpGet, Route("~/api/[controller]/GetOccupation")]
        public IActionResult GetOccupation()
        {
            return Ok(utilityBL.GetOccupation(
          HttpContext.User.FindFirst(c => c.Type == "RId").Value,
              HttpContext.User.FindFirst(c => c.Type == "Id").Value));
        }



        [HttpGet, Route("~/api/[controller]/GetMaritalstatus")]
        public IActionResult GetMaritalstatus()
        {
            return Ok(utilityBL.GetMaritalstatus(
          HttpContext.User.FindFirst(c => c.Type == "RId").Value,
              HttpContext.User.FindFirst(c => c.Type == "Id").Value));
        }


        // POST: api/Utility
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Utility/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
