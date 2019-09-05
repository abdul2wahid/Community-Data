using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Arabiceducation
    {
        public Arabiceducation()
        {
            Customers = new HashSet<Customers>();
        }

        public int ArabicEducationId { get; set; }
        public string ArabicEducationName { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }
    }
}
