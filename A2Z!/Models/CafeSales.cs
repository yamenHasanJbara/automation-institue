using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class CafeSales
    {
        [Key]

        public int CafeSales_Id { get; set; }

        public int AmountOfSaleDrink { get; set; } = 1;

        public DateTime date { get; set; }

        public Caffe Caffe { get; set; }
    }
}
