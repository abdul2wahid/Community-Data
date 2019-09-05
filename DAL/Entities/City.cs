using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class City
    {
        public City()
        {
            CustomerAddress = new HashSet<CustomerAddress>();
        }

        public int CityId { get; set; }
        public string City1 { get; set; }

        public virtual ICollection<CustomerAddress> CustomerAddress { get; set; }
    }
}
