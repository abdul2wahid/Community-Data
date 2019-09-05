using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Pincode
    {
        public Pincode()
        {
            CustomerAddress = new HashSet<CustomerAddress>();
        }

        public int PinId { get; set; }
        public string Pin { get; set; }

        public virtual ICollection<CustomerAddress> CustomerAddress { get; set; }
    }
}
