using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class ShowAllTeacherDetails
    {
        public int Course_Id { get; set; }

        public string teacher { get; set; }

        public string materialStudy { get; set; }

        public bool Isfinished { get; set; }

        public int coursePercent { get; set; }

        public string Group { get; set; }

        public int Price { get; set; }

        public List<Payment> payments { get; set; }

        public int TeacherAmount { get; set; }

        public int studentCount { get; set; }

        public int TeacherDeserved { get; set; }
    }
}
