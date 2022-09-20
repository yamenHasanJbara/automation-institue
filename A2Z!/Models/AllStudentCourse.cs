using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class AllStudentCourse
    {

        public string CourseName { get; set; }

        public int CoursePrice { get; set; }

        public string CourseGroup { get; set; }

        public bool IsFinished { get; set; }

        public int AmountPaid { get; set; }

        public int AmountStayed { get; set; }

        // belongs to teacher only it depends on student actual payments for course
        public int ActualDeserved { get; set; }
    }
}
