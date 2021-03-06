﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RumsBokning.Models
{
    public class IndexVM
    {
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name ="E-post")]
        [Required(ErrorMessage = "Enter a email")]
        public string UserName { get; set; }

        [Display(Name = "Lösenord")]
        [Required(ErrorMessage = "Enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
