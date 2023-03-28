
using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Project.Models
{
    public class IndexViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,3}$", ErrorMessage = "Please Provide Valid Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password should contain atleast 8 charachter")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password should contain atleast one Capital letter , one small case letter, one Digit and one special symbol")]
        public string Password { get; set; } = null!;

       
    }
}
