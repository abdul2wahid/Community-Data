using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Mulaqat
    {
        public int UserId { get; set; }
        public int? LastMetUserId { get; set; }
        public DateTime? LastMetDate { get; set; }

        public virtual Customers LastMetUser { get; set; }
        public virtual Customers User { get; set; }
    }
}
