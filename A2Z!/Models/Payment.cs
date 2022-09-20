using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class Payment
    {
        [Key]
        public int Payment_Id { get; set; }

        public int Amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        public Student  student { get; set; }

        public Teacher  teacher { get; set; }

        public Course course { get; set; }

        public int Payment_Type { get; set; } // if 1 then is teacher, 2 is Student

        public int Stayed { get; set; }

        public string PillNumber { get; set; }

    }
}
