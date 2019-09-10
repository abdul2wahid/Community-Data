using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class CustomerDetails:CustomerModel
    {
        public CustomerDetails()
        {

        }


        public string DOB { get; set; }
        public DateTime DateFormatDOB { get; set; }
        public int? GenderId { get; set; }
        public int? MaritalStatusId { get; set; }

        public int? OccupationId { get; set; }
        public int? educationId { get; set; }
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
        public int? StateId { get; set; }
        public int? PinId { get; set; }
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


