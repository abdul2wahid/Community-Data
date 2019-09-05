using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UpdatePasswordModel: UserModel
    {

        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
        public string oldPassword { get; set; }
    }
}
