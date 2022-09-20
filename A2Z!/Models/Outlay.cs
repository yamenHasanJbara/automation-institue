using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class Outlay
    {
        [Key]
        public int Outlay_Id { get; set; }

        public int Amount { get; set; }

        public int Outlay_Type { get; set; } // if 1 the pay دفع, if 2 then قبض

        public DateTime date { get; set; }

        public String Note { get; set; }

        public TypyOfOutlayPayment TypyOfOutlayPayment { get; set; }
    }
}
