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
        
        
    }
}
