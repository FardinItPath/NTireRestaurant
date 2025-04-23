using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be at least 3 characters long.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters long.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Role selection is required.")]
        public int RoleId { get; set; }

        public string? RoleName { get; set; }
    }
}
