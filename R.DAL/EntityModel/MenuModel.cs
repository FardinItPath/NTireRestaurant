using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.DAL.EntityModel
{
    public class MenuModel
    {
        [Key]
        public int MenuId { get; set; }

        [Required(ErrorMessage = "Menu name is required.")]
        [StringLength(255, MinimumLength = 4, ErrorMessage = "Menu name must be between 4 and 255 characters.")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 99999.99, ErrorMessage = "Price must be between $0.01 and $99,999.99.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Category selection is required.")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryModel? Category { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDT { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedDT { get; set; }
    }
}
