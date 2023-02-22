using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Data.Entities
{
    public class Order
    {
        [Key]
        public int Guid { get; set; }
        [Required]
        public DateTime CreationDateAndTime { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Status { get; set; }
        public virtual List<Product> Products { get; set; }

    }
}
