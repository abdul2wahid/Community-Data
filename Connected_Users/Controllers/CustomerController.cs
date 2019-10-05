using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buisness_Layer;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using OfficeOpenXml;

namespace Connected_Users.Controllers
{
    [Authorize(Policy = "AllLoggedInUsersPolicy")]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        Customer_BL bl;

        public CustomerController()
        {
            bl = new Customer_BL();
        }


        //[Authorize("EmployeeOnly")]
        // GET api/values
      
        [HttpGet]
        public IActionResult Get(string sortOrder, int currentPageNo, string filterString)
        {
            int count=0;
           
            List<CustomerModel> list = bl.GetCustomers(sortOrder, currentPageNo, filterString, Startup.PageSize,out count);

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
                    Count =count,
                    pageIndex = currentPageNo,
                    pageSize = Startup.PageSize,
                    items = new List<CustomerModel>(),
                };
                return (Ok( obj));
            }

        }

        // GET api/values/5
        [HttpGet("{id}"), Route("~/api/[controller]/Detail")]
        public List<CustomerDetails> Get(int id)
        {
            return bl.GetCustomerDetails(id);
        }

        [Authorize(Policy = "SuperUser&AdminPolicy")]
        [HttpGet("{id}"), Route("~/api/[controller]/Find")]
        public int Get(string userName,string DOB)
        {
            return bl.FindCustomer(userName, DOB);
        }


        [Authorize(Policy = "SuperUser&AdminPolicy")]
        [HttpGet("{id}"), Route("~/api/[controller]/DownloadCustomers")]
        public IActionResult Get()
        {

            var comlumHeadrs = new string[]
          {
                "Name",
                "Gender",
                "Age",
                "Marital Status",
                "Mobile Number",

                 "Occupation",
                "OccupationDetails",

                "arabicEducationName",
                "educationName",
                "EducationDetails",

               "Address1",
               "Address2",
               "Area",
               "City" ,
               "State",
               "pin"

          };

            byte[] result;

            using (var package = new ExcelPackage())
            {
                // add a new worksheet to the empty workbook
                var worksheet = package.Workbook.Worksheets.Add("users"); //Worksheet name
                using (var cells = worksheet.Cells[1, 1, 1, comlumHeadrs.Length+1]) //(1,1) (1,0)
                {
                    cells.Style.Font.Bold = true;
                    cells.AutoFilter = true;
                }

                //First add the headers
                for (var i = 0; i < comlumHeadrs.Count(); i++)
                {
                    worksheet.Cells[1, i + 1].Value = comlumHeadrs[i];
                }

                //Add values
                var j = 2;
                int count = 0;
                foreach (CustomerDetails cModel in bl.GetCustomerDetails())
                {
             
                    worksheet.Cells["A" + j].Value = cModel.Name;
                    worksheet.Cells["B" + j].Value = cModel.Gender;
                    worksheet.Cells["C" + j].Value = cModel.Age;
                    worksheet.Cells["D" + j].Value = cModel.MaritalStatus;
                    worksheet.Cells["E" + j].Value = cModel.MobileNumber;


                    worksheet.Cells["F" + j].Value = cModel.Occupation;
                    worksheet.Cells["G" + j].Value = cModel.OccupationDetails;
                  

                    worksheet.Cells["H" + j].Value = cModel.arabicEducationName;
                    worksheet.Cells["I" + j].Value = cModel.educationName;
                    worksheet.Cells["J" + j].Value = cModel.EducationDetails;

                    worksheet.Cells["K" + j].Value = cModel.Address1;
                    worksheet.Cells["L" + j].Value = cModel.Address2;
                    worksheet.Cells["M" + j].Value = cModel.Area;
                    worksheet.Cells["N" + j].Value = cModel.City;
                    worksheet.Cells["O" + j].Value = cModel.State;
                    worksheet.Cells["P" + j].Value = cModel.Pin;

                    j++;
                }
                result = package.GetAsByteArray();
            }

            return File(result, "application/ms-excel", $"Customers_Data.xlsx");
             
        }




        
        // POST api/values
        [HttpPost]
        public bool Post([FromBody]CustomerDetails cust)
        {

            if (ModelState.IsValid)
            {

                return bl.AddCustomer(cust, HttpContext.User.FindFirst(c => c.Type == "RId").Value,
                    HttpContext.User.FindFirst(c => c.Type == "Id").Value);
            }
            return false;

        }

        [Authorize(Policy = "SuperUser&AdminPolicy")]
        // PUT api/values/5
        [HttpPut("{id}"), Route("~/api/[controller]/Update")]
        public bool Put([FromBody] List<CustomerDetails> cust)
        {
            if (ModelState.IsValid)
            {

                return bl.UpdateCustomer(cust, HttpContext.User.FindFirst(c => c.Type == "RId").Value,
                 HttpContext.User.FindFirst(c => c.Type == "Id").Value);
               
            }
            else
            {
                return false;
            }
        }

        [Authorize(Policy = "SuperUser&AdminPolicy")]
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return bl.DeleteCustomer(id);
        }
    }
}
