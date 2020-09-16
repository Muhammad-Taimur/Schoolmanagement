﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Models.ViewModels
{
    public class StudentViewModel
    {
        public string FName { get; set; }
        public string LName { get; set; }

        [Display(Name = "DOB")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

    }
}
