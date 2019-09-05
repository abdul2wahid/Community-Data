using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Maritalstatus
    {
        public Maritalstatus()
        {
            Customers = new HashSet<Customers>();
        }

        public int MaritalStatusId { get; set; }
        public string MaritalStatus1 { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }
    }
}
