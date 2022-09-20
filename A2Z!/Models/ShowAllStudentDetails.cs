using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class ShowAllStudentDetails
    {
        public int Student_Id { get; set; }

        public string StudnetName { get; set; }

        public List<AllStudentCourse> allStudentCourses { get; set; }
    }
}
