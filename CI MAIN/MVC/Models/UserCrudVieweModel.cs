using CI_PLatform_Entities.Models;

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

        public List<MissionTheme> MissionThemes { get; set; }

        public List<Skill> Skills { get; set;}

        public long? CityId { get; set; }

        public long? CountryId { get; set; }
    }
}
