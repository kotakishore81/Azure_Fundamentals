﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Azure_Blob.Models
{
    public class Container
    {
        [Required]
        public string Name { get; set; }
    }
}
