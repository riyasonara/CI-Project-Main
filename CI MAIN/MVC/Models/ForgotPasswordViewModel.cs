using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Project.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,3}$", ErrorMessage = "Please Provide Valid Email")]
        public string Email { get; set; }
    }
}
