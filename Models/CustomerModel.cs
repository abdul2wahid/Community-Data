using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class CustomerModel
    {
        [Required(ErrorMessage = "CustomerID is required")]
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        [Required(ErrorMessage = "Occupation is required")]
        public string Occupation { get; set; }
        [Required(ErrorMessage = "MobileNumber is required")]
        public string MobileNumber { get; set; }

    }
}
