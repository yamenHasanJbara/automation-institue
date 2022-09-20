using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Healpers
{
    public class IntegerValidation
    {
        public static bool checkIntValue(string value)
        {
            int number;
            if (int.TryParse(value, out number))
            {
                return true;
            }
            return false;
        }
    }
}
