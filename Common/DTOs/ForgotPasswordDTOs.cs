using System.ComponentModel.DataAnnotations;

namespace Common.ViewModel
{
    public class ForgotPasswordDTOs
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
    }
}
