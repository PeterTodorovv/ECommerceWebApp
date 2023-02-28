using e_Commerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Services.Responses
{
    public class ProductAllResponse
    {
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public int ProductsCount { get; set; }

    }
}
