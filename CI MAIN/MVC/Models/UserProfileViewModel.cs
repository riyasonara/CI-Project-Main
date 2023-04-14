using CI_PLatform_Entities.Models;

namespace CI_Platform_Project.Models
{
    public class UserProfileViewModel
    {
        public string FirstName { get; set; }

        public string? Surname { get; set; }

        public string Email { get; set; } = null!;

        public string employeeID { get; set; }

        public string? Avatar { get; set; }

        public string Availability { get; set; }


        public string title { get; set; }

        public string department { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? WhyIVolunteer { get; set; }

        public string? ProfileText { get; set; }

        public long? CityId { get; set; }

        public long? CountryId { get; set; }

        public string? LinkedinUrl { get; set; }


        public List<Skill> skills {  get; set; }
        public List<UserSkill> userSkills {  get; set; }
        public List<Skill> RemainingSkill {  get; set; }
    }
}
