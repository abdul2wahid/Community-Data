using System;

namespace Models
{
    public class UserModel
    {

        public string UserName { get; set; }
        public string DOB { get; set; }
        public int  UserId { get; set; }
        public string CreatedBy { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
    }
}
