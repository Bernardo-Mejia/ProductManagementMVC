using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        [MaxLength(5, ErrorMessage = "Postal Code must have a maximun of 5 characters.")]
        [MinLength(5, ErrorMessage = "Postal Code must have a minimum of 5 characters.")]
        public string? PostalCode { get; set; }
    }
}
