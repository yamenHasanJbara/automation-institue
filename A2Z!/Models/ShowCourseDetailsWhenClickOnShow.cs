using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class ShowCourseDetailsWhenClickOnShow
    {
        public Course course { get; set; }

        public DateTime Start_Date { get; set; }

        public DateTime End_Date { get; set; }

        public List<ProgramInstituteC> programInstituteCs { get; set; }

        public List<ProgramInstituteOneAndHalfHour> programInstituteOneAndHalfHours { get; set; }
    }
}
