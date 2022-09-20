﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    public class ProgramInstituteOneAndHalfHour
    {
        [Key]
        public int ProgramInstitute_Id { get; set; }

        public Course course { get; set; }

        public string day { get; set; }

        public string hour { get; set; }

        public string hall { get; set; }
    }
}
