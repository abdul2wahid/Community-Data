using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Education
    {
        public Education()
        {
            Customers = new HashSet<Customers>();
        }

        public int EducationId { get; set; }
        public string EducationName { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }
    }
}
