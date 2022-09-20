using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class Teacher
    {
        [Key]
        public int Teacher_Id { get; set; }

        public String Name { get; set; }

        public String Number_Phone { get; set; }

        public ICollection<Course> courses { get; set; }

        public ICollection<Payment> payments { get; set; }


    }
}
