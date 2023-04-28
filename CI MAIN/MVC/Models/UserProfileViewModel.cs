using CI_PLatform_Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Project.Models
{
    public class UserProfileViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First Name Should be min 2 and max 20 length")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Last Name Should be min 5 and max 20 length")]
        public string? Surname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,3}$", ErrorMessage = "Please Provide Valid Email")]
        public string Email { get; set; } = null!;

        public string employeeID { get; set; }

        public string? Avatar { get; set; }

        public string Availability { get; set; }


        public string title { get; set; }

        public string department { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? WhyIVolunteer { get; set; }


        [Required]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "Profile text Should be of minimum 10 and maximum 150 words")]
        public string? ProfileText { get; set; }

        public long? CityId { get; set; }


       
        [Required(AllowEmptyStrings = false,ErrorMessage = "Please select country")]
        public long? CountryId { get; set; }

        public string? LinkedinUrl { get; set; }

        public IFormFile UserImg { get; set; }

        public List<IFormFile> files { get; set; }


        public List<Skill> skills {  get; set; }
        public List<UserSkill> userSkills {  get; set; }
        public List<Skill> RemainingSkill {  get; set; }
     
        public string Password { get; set; } = null!;            
        public string newPassword { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string subject { get; set; }
        public string message { get; set; }


    }
}
