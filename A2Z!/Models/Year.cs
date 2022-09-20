using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class Year
    {

        [Key]
        public int Year_Id { get; set; }
        
        public int Year_Number { get; set; }

        public ICollection<Material_Study> Material_Studies { get; set; }

        public Faculty Faculty { get; set; }
    }
}
