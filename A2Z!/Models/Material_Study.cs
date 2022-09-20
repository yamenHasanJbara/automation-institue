using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class Material_Study
    {

        [Key]
        public int Material_Study_Id { get; set; }

        public String Name { get; set; }

        public int Semester { get; set; }

        public Year Year { get; set; }

        public Faculty Faculty { get; set; }

        public Section Section { get; set; }

    }
}
