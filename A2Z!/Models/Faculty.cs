using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class Faculty
    {
        [Key]
        public int Faculty_Id { get; set; }

        public String Name { get; set; }

        public ICollection<Year> Years { get; set; }

        public ICollection<Student> students { get; set; }

        public Section Section { get; set; }
    }
}
