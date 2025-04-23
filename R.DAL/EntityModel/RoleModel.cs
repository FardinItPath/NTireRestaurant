using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.DAL.EntityModel
{
    public class RoleModel
    {
        [Key]
        public int RoleId { get; set; }
        [Required, StringLength(50)]
        public string? RoleName { get; set; }
    }
}
