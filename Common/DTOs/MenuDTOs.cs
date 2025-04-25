using System.ComponentModel.DataAnnotations;

namespace Common.ViewModel
{
    public class MenuDTOs
    {
        public int MenuId { get; set; }

        [Required(ErrorMessage = "Menu name is required.")]
        [StringLength(255, ErrorMessage = "Menu name cannot exceed 255 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Menu description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 10000.00, ErrorMessage = "Price must be between $0.01 and $10,000.")]
        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Category selection is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category.")]
        public int CategoryId { get; set; }

        //public string? CategoryName { get; set; }
        //public IEnumerable<CategoryModel>? Categories { get; set; }
        public DateTime? UpdatedDT { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
