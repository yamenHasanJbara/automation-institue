using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class Caffe
    {
        [Key]

        public int Drink_Id { get; set; }

        public String DrinkName { get; set; }

        public int DrinkPrice { get; set; }

        public ICollection<CafeSales> CafeSales { get; set; }

    }
}
