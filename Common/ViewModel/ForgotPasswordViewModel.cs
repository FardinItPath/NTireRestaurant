using System.ComponentModel.DataAnnotations;

namespace Common.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
    }
}
