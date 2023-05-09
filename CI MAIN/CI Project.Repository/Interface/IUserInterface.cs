using CI_PLatform_Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Project.Repository.Interface
{
    public interface IUserInterface
    {
        public User UserByEmail(String mail);

        public User UserByEmail_Password(String mail, String Password);

        public List<PasswordReset> passwordreset();

        public List<User> users();

        public List<Mission> missionlist();

        public List<City> cities();

        public List<Country> countrylist();

        public List<MissionTheme> themelist();

        public List<Skill> skilllist();

        public List<GoalMission> goalMissions();

        public List<MissionRating> missionRatings();

        public void adduser(User user);

        public void userRating(MissionRating rating);

        public void userComment(Comment comment);

        public List<FavoriteMission> favoriteMissions();

        public List<MissionApplication> applications();
        public List<Comment> comments();

        public void passwordReset(PasswordReset pswdreset);

        public List<UserSkill> skilllist(int userid);

        public List<MissionApplication> missionApplications();

        public void AddUserSkills(long SkillId, int UserId);

        public void updateuser(User user);

        public void apply(long missionid, long userid);

        public ContactU addContactUs(string subject, string message, string FirstName,string Surname, string email);

        public User addUser(string avatar, string firstName, string lastName, string email, string password, string empid, string department, long cityId, long countryId, string ProfileText, int status);
        public User updateUser(string avatar, string firstName, string lastName, string email, string password, string empid, string department, long cityId, long countryId, string ProfileText, int status, long userId);
        public Mission addMission( string Title, string ShortDesc, string Desc, int city, int country, string OrgName, string OrgDetail, string misstype, int seats, DateTime startdate, DateTime endDate, DateTime RegDeadline, string availability, int themeid, int skill);

        public Mission updateMission(long missionId, string Title, string ShortDesc, string Desc, int city, int country, string OrgName, string OrgDetail, string misstype, int seats, DateTime startdate, DateTime endDate, DateTime RegDeadline, string availability, int themeid, int skill);

        public MissionTheme addtheme(string themeName, int Status);

        public MissionTheme updateTheme(string themeName, int Status, long MissionThemeId);

        public Skill addSkill(string skillName, int Status);

        public Skill updateSkill(string skillName, int Status, long skillId);

        //public User UserExist(string Email);

        public Banner AddBanner(string discrption, string image, int sortorder);

        public Banner UpdateBanner(string discrption, string image, int sortorder, long bannerId);

        public List<Banner> AllBanners();

        public void DeleteBanner(long bannerId);

    }
}
