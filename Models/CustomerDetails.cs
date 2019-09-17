using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class CustomerDetails:CustomerModel
    {
        public CustomerDetails()
        {

        }

        [Required(ErrorMessage = "DOB is required")]
        public string DOB { get; set; }
        public DateTime DateFormatDOB { get; set; }

        [Range(0,int.MaxValue,ErrorMessage = "GenderId invalid"),
            Required(ErrorMessage = "GenderId is required")]
        public int? GenderId { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "MaritalStatusId invalid"),
                  Required(ErrorMessage = "MaritalStatusId is required")]
        public int? MaritalStatusId { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "OccupationId invalid"),
                 Required(ErrorMessage = "OccupationId is required")]
        public int? OccupationId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "educationId invalid"),
             Required(ErrorMessage = "educationId is required")]
        public int? educationId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "arabicEducationID invalid"),
             Required(ErrorMessage = "arabicEducationID is required")]
        public int? arabicEducationID{ get; set; }

        public string educationName { get; set; }
        public string arabicEducationName{ get; set; }
        


        public string OccupationDetails { get; set; }
        public string EducationDetails { get; set; }


        public int? UpdatedBy { get; set; }
        public int? CreatedBy{get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn{ get; set; }


        //RelationShip
        public int? ChildrenId { get; set; }
        public int? WifeId { get; set; }


        //Address
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Area { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "StateId invalid"),
             Required(ErrorMessage = "StateId is required")]
        public int? StateId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "PinId invalid"),
             Required(ErrorMessage = "PinId is required")]
        public int? PinId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "CityId invalid"),
             Required(ErrorMessage = "CityId is required")]
        public int? CityId { get; set; }

        public string State { get; set; }
        public string Pin { get; set; }
        public string City  { get; set; }


        //To remove the current user as dependant to others 
        public bool? DependantToBeDeleted { get; set; }


        //To add dependant
        public int? DependantParentID { get; set; }
        public bool? DependantToBeAddedAsChild { get; set; }


    }
}


