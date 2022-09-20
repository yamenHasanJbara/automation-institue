using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace A2Z_.Models
{
    public class Show_Admin_Course_Details
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public string Group { get; set; }

        public int CoursePrice { get; set; }

        public bool IsFinshed { get; set; }

        public int Count { get; set; }

        public string Collage { get; set; }

        public int year { get; set; }

        public int Semester { get; set; }

        public string TeacherName { get; set; }

        public int TeacherPercent { get; set; }

        public int InstituePercent { get; set; }

        public int ActualPaid { get; set; }

        public int InstituteAmount { get; set; }

    }
}
