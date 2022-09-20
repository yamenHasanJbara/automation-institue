using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class Show_Student_details_When_DoubleClickOnCourse
    {
        public int Student_Id { get; set; }

        public string Student_Name { get; set; }

        public string Student_Phone { get; set; }

        public string CourseName { get; set; }

        public int CoursePrice { get; set; }

        public int AmountPaid { get; set; }

        public int AmountStayed { get; set; }

        public bool Withdrawn { get; set; }
    }
}
