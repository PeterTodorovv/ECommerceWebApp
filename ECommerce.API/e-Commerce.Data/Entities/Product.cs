using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Data.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
        public string ImageAddress{ get; set; }
        public string Description { get; set; }

        public virtual List<Order> Orders { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
