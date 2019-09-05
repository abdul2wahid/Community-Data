using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Gender
    {
        public Gender()
        {
            Customers = new HashSet<Customers>();
        }

        public int GenderId { get; set; }
        public string Gender1 { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }
    }
}
