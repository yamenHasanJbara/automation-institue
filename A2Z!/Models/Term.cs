using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class Term
    {
        [Key]
        public int Terms_id { get; set; }

        public string name { get; set; }
        
        [Column(TypeName = "date")]
        public DateTime start_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime end_date { get; set; }

        public ICollection<Course> courses { get; set; }
    }
}
