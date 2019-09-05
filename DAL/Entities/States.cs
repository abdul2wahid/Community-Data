using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class States
    {
        public States()
        {
            CustomerAddress = new HashSet<CustomerAddress>();
        }

        public int StateId { get; set; }
        public string State { get; set; }

        public virtual ICollection<CustomerAddress> CustomerAddress { get; set; }
    }
}
