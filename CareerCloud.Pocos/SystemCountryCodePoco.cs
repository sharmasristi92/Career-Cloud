﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("System_Country_Codes")]
    public class SystemCountryCodePoco
    {
        [Key]
        public String Code { get; set; }
        public String Name { get; set; }
    }
}
