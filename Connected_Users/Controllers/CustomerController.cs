﻿using System;
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
        public List<CustomerModel> Get(string sortOrder,int currentPageNo,string filterString)
        {
            return bl.GetCustomers(sortOrder,currentPageNo,filterString);
        }

        // GET api/values/5
        [HttpGet("{id}"), Route("~/api/[controller]/Detail")]
        public List<CustomerDetails> Get(int id)
        {
            return bl.GetCustomerDetails(id);
        }

        [HttpGet("{id}"), Route("~/api/[controller]/Find")]
        public int Get(string userName,string DOB)
        {
            return bl.FindCustomer(userName, DOB);
        }


        [HttpGet("{id}"), Route("~/api/[controller]/DownloadCustomers")]
        public IActionResult Get()
        {

            var comlumHeadrs = new string[]
          {
                "Customer Id",
                "Name",
                "Gender",
                "Age",
                "Marital Status",
                "Occupation",
                "Mobile Number"
          };

            byte[] result;

            using (var package = new ExcelPackage())
            {
                // add a new worksheet to the empty workbook

                var worksheet = package.Workbook.Worksheets.Add("Customer data"); //Worksheet name
                using (var cells = worksheet.Cells[1, 1, 1, 5]) //(1,1) (1,5)
                {
                    cells.Style.Font.Bold = true;
                }

                //First add the headers
                for (var i = 0; i < comlumHeadrs.Count(); i++)
                {
                    worksheet.Cells[1, i + 1].Value = comlumHeadrs[i];
                }

                //Add values
                var j = 2;
                foreach (CustomerModel cModel in bl.GetCustomers("",-1,""))
                {
                    worksheet.Cells["A" + j].Value = cModel.CustomerID;
                    worksheet.Cells["B" + j].Value = cModel.Name;
                    worksheet.Cells["C" + j].Value = cModel.Gender;
                    worksheet.Cells["D" + j].Value = cModel.Age;
                    worksheet.Cells["E" + j].Value = cModel.MaritalStatus;
                    worksheet.Cells["F" + j].Value = cModel.Occupation;
                    worksheet.Cells["G" + j].Value = cModel.MobileNumber;

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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return bl.DeleteCustomer(id);
        }
    }
}
