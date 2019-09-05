using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Customers
    {
        public Customers()
        {
            Events = new HashSet<Events>();
            MulaqatLastMetUser = new HashSet<Mulaqat>();
            ReltionshipChildern = new HashSet<Reltionship>();
            ReltionshipUser = new HashSet<Reltionship>();
            ReltionshipWife = new HashSet<Reltionship>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public int GenderId { get; set; }
        public int MaritalStatusId { get; set; }
        public string MobileNumber { get; set; }
        public int? OccupationId { get; set; }
        public int? ArabicEducationId { get; set; }
        public int? EducationId { get; set; }
        public string EducationDetail { get; set; }
        public string OccupdationDetails { get; set; }
        public int? UpdatedBy { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }

        public virtual Arabiceducation ArabicEducation { get; set; }
        public virtual Education Education { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Maritalstatus MaritalStatus { get; set; }
        public virtual Occupation Occupation { get; set; }
        public virtual CustomerAddress CustomerAddress { get; set; }
        public virtual Mulaqat MulaqatUser { get; set; }
        public virtual Users Users { get; set; }
        public virtual ICollection<Events> Events { get; set; }
        public virtual ICollection<Mulaqat> MulaqatLastMetUser { get; set; }
        public virtual ICollection<Reltionship> ReltionshipChildern { get; set; }
        public virtual ICollection<Reltionship> ReltionshipUser { get; set; }
        public virtual ICollection<Reltionship> ReltionshipWife { get; set; }
    }
}
