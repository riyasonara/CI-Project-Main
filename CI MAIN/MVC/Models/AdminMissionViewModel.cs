using CI_PLatform_Entities.Models;
//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Project.Models
{
    public class AdminMissionViewModel
    {
        public long missionId { get; set; }
        public List<Mission> Missions { get; set; }

        public List<City> Cities { get; set; }
        public List<Country> Countries { get; set; }

        public List<MissionTheme> MissionThemes { get; set; }

        public List<Skill> Skills { get; set; }

        [Required(ErrorMessage ="enter mission type")]
        public string MissionType { get; set; }

        [Required(ErrorMessage ="title is required")]
        [RegularExpression(@"^([a-zA-Z0-9 \.\&\'\-]+)$", ErrorMessage = "Invalid title")]
        public string Title { get; set; } = null!;


        [Required(ErrorMessage ="short description required")]
        public string? ShortDescription { get; set; }

        [Required (ErrorMessage ="Description required")]
        public string? Description { get; set; }

        [Required (ErrorMessage ="select city")]
        public long CityId { get; set; }

        [Required(ErrorMessage ="select country")]
        public long CountryId { get; set; }

        [Required(ErrorMessage ="enter organization name")]
        public string? OrganizationName { get; set; }

        [Required(ErrorMessage ="enter organization detail")]
        public string? OrganizationDetail { get; set; }

        [Required(ErrorMessage ="enter seats left")]
        public long? SeatsLeft { get; set; }

        public DateTime? Deadline { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage ="enter available seats")]
        public string? Availability { get; set; }

        public List<MissionMedium> ImageFiles { get; set;}

        public List<IFormFile> docfiles { get; set; }

        public string url { get; set; }

    }
}
