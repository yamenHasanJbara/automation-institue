using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class TypyOfOutlayPayment
    {

        [Key]

        public int TypyOfOutlayPayment_Id { get; set; }

        public String type { get; set; }

        public ICollection<Outlay> Outlays { get; set; }

    }
}
