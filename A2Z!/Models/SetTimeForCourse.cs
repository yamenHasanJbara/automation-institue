using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public  class SetTimeForCourse
    {
        [Key]
        public int SetTimeForCourse_Id { get; set; }

        public string Courselength { get; set; } = "2";// default time for course length is 2 hours;

        public string StartTime { get; set; } = "8";
    }
}
