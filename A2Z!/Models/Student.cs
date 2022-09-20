using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class Student
    {
        [Key]
        public int Student_Id { get; set; }

        public String Name { get; set; }

        public String Number_Phone { get; set; }

        public ICollection<Payment> payments { get; set; }

        public ICollection<Student_course> Student_Courses { get; set; }
    }
}
