using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace A2Z_.Models
{
    public class Course
    {

        [Key]
        public int Course_Id { get; set; }

        public Section section { get; set; }

        public Faculty faculty { get; set; }

        public Year Year { get; set; }

        public Material_Study material_Study { get; set; }

        public Teacher teacher { get; set; }

        public Term term { get; set; }

        public ICollection<Student_course> Student_Courses { get; set; }

        public ICollection<Payment> payments { get; set; }

        public String Group { get; set; }

        public int Price { get; set; }

        [Column(TypeName ="date")]
        public DateTime Start_Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime End_Date { get; set; }

        public int percent { get; set; }

        public bool IsFinished { get; set; } = false;

    }
}
