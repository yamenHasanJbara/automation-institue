using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }

        public String UserName { get; set; }

        public String Password { get; set; }

        public int permission { get; set; } = 2; // permession 1 is admin, 2 is regular user

        public int Status { get; set; } = 1; // 1 is not active user, 2 is the current user who is use the program
    }
}
