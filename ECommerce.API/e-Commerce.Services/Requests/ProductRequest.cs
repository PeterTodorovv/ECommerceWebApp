﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Services.Requests
{
    public class ProductRequest
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageAddress { get; set; }
    }
}
