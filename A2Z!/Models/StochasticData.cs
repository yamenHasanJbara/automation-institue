using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class StochasticData
    {
        public Section  section { get; set; }

        public Faculty faculty { get; set; }

        public Year year { get; set; }

        public int Semester { get; set; }

        public Material_Study material_Study { get; set; }

        public Term term { get; set; }
        public long  Count { get; set; }

        public long ActualDeserved { get; set; }

        public long InstituteDeserved { get; set; }

        public long TeacherDeserved { get; set; }

        public long StudentTaken { get; set; }

        public long TeacherGiven { get; set; }

        public long Difference { get; set; }

        // added lately نسبة الاستاذ الفعلية = دفوعات الطلاب * نسبة الكورس / 100
        public long TeacherActualDeserved { get; set; }
    }
}
