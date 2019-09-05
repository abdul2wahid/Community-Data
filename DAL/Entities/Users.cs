using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int? UpdatedBy { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }

        public virtual Roles Role { get; set; }
        public virtual Customers User { get; set; }
    }
}
