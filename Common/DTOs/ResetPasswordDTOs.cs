using System.ComponentModel.DataAnnotations;

namespace Common.ViewModel
{
    public class ResetPasswordDTOs
    {
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        [StringLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }
    }
}
