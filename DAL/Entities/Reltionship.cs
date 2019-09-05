using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Reltionship
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? ChildernId { get; set; }
        public int? WifeId { get; set; }

        public virtual Customers Childern { get; set; }
        public virtual Customers User { get; set; }
        public virtual Customers Wife { get; set; }
    }
}
