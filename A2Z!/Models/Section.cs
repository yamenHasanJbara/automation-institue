using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class Section
    {
        [Key]
        public int Section_Id { get; set; }

        public String Section_Name { get; set; }

        public ICollection<Faculty> Faculties { get; set; }
    }
}
