using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class Student_course
    {

        [Key]
        public int Student_Course_Id { get; set; }

        public int Course_Id { get; set; }

        public int Student_Id { get; set; }

        [ForeignKey(nameof(Course_Id))]
        public Course Course_Id_FK { get; set; }

        [ForeignKey(nameof(Student_Id))]
        public Student Student_Id_FK { get; set; }
        
        [DefaultValue(false)]
        public bool Withdrawn { get; set; }

    }
}
