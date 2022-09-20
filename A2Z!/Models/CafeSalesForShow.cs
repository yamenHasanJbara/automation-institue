using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace A2Z_.Models
{
    public class CafeSalesForShow
    {

        [Key]

        public int CafeSales_Id { get; set; }

        public int AmountOfSaleDrink { get; set; } = 1;

        public DateTime date { get; set; }

        public string DrinkName { get; set; }

        public int DrinkPrice { get; set; }
    }
}
