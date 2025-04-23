
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.DAL.EntityModel
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, StringLength(100)]
        public string? CategoryName { get; set; }
        //public virtual ICollection<MenuModel> Menus { get; set; }
    }
}
