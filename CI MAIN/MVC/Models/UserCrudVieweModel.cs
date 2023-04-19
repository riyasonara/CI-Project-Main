using CI_PLatform_Entities.Models;

namespace CI_Platform_Project.Models
{
    public class UserCrudVieweModel
    {

        public List<User> users = new List<User>();

        public List<CmsPage> pages = new List<CmsPage>(); 

        public List<Mission> missions = new List<Mission>();

        public List<MissionApplication> MissionApplications = new List<MissionApplication>();
    }
}
