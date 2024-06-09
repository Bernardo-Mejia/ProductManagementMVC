using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Field {0} is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Field {0} is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Field {0} is required.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Field {0} is required.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Field {0} is required.")]
        [Display(Name = "List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }

        [Required(ErrorMessage = "Field {0} is required.")]
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }

        [Required(ErrorMessage = "Field {0} is required.")]
        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }

        [Required(ErrorMessage = "Field {0} is required.")]
        [Display(Name = "Price for 100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }


    }
}
