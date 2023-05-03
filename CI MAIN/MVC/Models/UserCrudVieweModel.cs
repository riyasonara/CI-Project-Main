using CI_PLatform_Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Project.Models
{
    public class UserCrudVieweModel
    {

        public List<User> users { get; set; }

        public List<CmsPage> pages { get; set; }


        public List<Mission> missions { get; set; }

        public List<MissionApplication> MissionApplications { get; set; }

        public List<Story> Stories { get; set; }

        public List<City> Cities { get; set; }

        public List<Country> Countries { get; set; }
       
        public List<Skill> Skills { get; set; }

        public List<MissionTheme> MissionThemes { get; set; }

        public long UserId { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First Name Should be min 2 and max 20 length")] 
        public string? FirstName { get; set; }

        [StringLength(20, MinimumLength = 5, ErrorMessage = "Last Name Should be min 5 and max 20 length")]
        public string? LastName { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,3}$", ErrorMessage = "Please Provide Valid Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password should contain atleast 8 charachter")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password should contain atleast one Capital letter , one small case letter, one Digit and one special symbol")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Phone Number not valid")]
        public long PhoneNumber { get; set; }

        [Required]
        public string? Avatar { get; set; }

        [Required(ErrorMessage = "Enter why you volunteer")]
        public string? WhyIVolunteer { get; set; }

        [Required]
        public string? EmployeeId { get; set; }

        [Required]
        public string? Department { get; set; }

        [Required]
        public long? CityId { get; set; }

        [Required]
        public long? CountryId { get; set; }

        [Required]
        public string? ProfileText { get; set; }

        [Required(ErrorMessage ="Enter your LinkedIn Profile URL")]
        public string? LinkedInUrl { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public int Status { get; set; }

        public DateTime? CreatedAt { get; set; } = null!;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        [Required]
        public string? Availability { get; set; }

        public string avatar { get; set; }




    }
}
