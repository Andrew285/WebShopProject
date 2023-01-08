﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class CoverType
    {
        [Key]
        public int Id;
        [Required]
        public string Name;
    }
}