using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class Show_Student_Details_Payments
    {

        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public bool IsFinished { get; set; }

        public int  CoursePrice { get; set; }

        public string CourseGroup { get; set; }

        public int Amount { get; set; }

        public int Amountstaied { get; set; }

        public string Term { get; set; }

        public bool Withdrawn { get; set; }

    }
}
