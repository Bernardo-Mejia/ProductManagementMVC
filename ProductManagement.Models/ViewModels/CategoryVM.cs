using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Models.ViewModels
{
    public class CategoryVM
    {
        public Category Category { get; set; }
        public List<Product> Products { get; set; }
    }
}
