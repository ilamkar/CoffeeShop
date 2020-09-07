using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models
{
    public class Quote
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

       
    }
}
