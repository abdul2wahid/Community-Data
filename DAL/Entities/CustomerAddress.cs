using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class CustomerAddress
    {
        public int CustomerId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Area { get; set; }
        public int? StateId { get; set; }
        public int? PinId { get; set; }
        public int? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual Customers Customer { get; set; }
        public virtual Pincode Pin { get; set; }
        public virtual States State { get; set; }
    }
}
