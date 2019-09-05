using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Occupation
    {
        public Occupation()
        {
            Customers = new HashSet<Customers>();
        }

        public int OccupationId { get; set; }
        public string OccuptionName { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }
    }
}
