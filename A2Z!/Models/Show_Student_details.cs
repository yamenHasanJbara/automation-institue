using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class Show_Student_details
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public List<Show_Student_Details_Payments> show_Student_Details_Payments { get; set; }
    }
}
