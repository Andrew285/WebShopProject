using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1, 100000)]
        public double ListPrice { get; set; }

        [Required]
        [Range(1, 100000)]
        public double Price { get; set; }

        [Required]
        [Range(1, 100000)]
        public double SpecialPrice5 { get; set; }

        [Required]
        [Range(1, 100000)]
        public double SpecialPrice10 { get; set; }

        public string ImageUrl { get; set; } 
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public int CoverTypeId { get; set; }
        public CoverType CoverType { get; set; }
    }
}
