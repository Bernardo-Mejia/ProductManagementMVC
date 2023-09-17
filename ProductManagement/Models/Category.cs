using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is a field required")]
        [MaxLength(30, ErrorMessage = "The max lenght to {0} is {1}")]
        [DisplayName("Category Name")]
        public string? Name { get; set; }
        [DisplayName("Display order")]
        [Range(1, 1001, ErrorMessage = "{0} must be between {1}-{2}")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
