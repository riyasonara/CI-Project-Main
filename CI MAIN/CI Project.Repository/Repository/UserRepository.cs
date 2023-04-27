using CI_PLatform_Entities.CIDbContext;
using CI_PLatform_Entities.Models;
using CI_Project.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Project.Repository.Repository
{
    public class UserRepository : IUserInterface
    {
        private readonly CiPlatformContext _db;

        public UserRepository(CiPlatformContext db)
        {
            _db = db;
        }
        public User UserByEmail(String mail)
        {
            return _db.Users.FirstOrDefault(u => u.Email == mail);
        }

        public User UserByEmail_Password(String mail, String Password)
        {
            return _db.Users.FirstOrDefault(m => m.Email == mail && m.Password == Password);
        }
        public List<PasswordReset> passwordreset()
        {
            return _db.PasswordResets.ToList();
        }
        public List<User> users()
        {
            return _db.Users.ToList();
        }
        public List<Mission> missionlist()
        {
            return _db.Missions.ToList();
        }
        public List<City> cities()
        {
            return _db.Cities.ToList();
        }
        public List<Country> countrylist()
        {
            return _db.Countries.ToList();
        }
        public List<MissionTheme> themelist()
        {
            return _db.MissionThemes.ToList();
        }
        public List<Skill> skilllist()
        {
            return _db.Skills.ToList();
        }
        public List<UserSkill> skilllist(int userid)
        {
            return _db.UserSkills.Where(e => e.UserId == userid).ToList();
        }
        public List<GoalMission> goalMissions()
        {
            return _db.GoalMissions.ToList();
        }

        public List<MissionRating> missionRatings()
        {
            return _db.MissionRatings.ToList();
        }

        public void apply(long missionid, long userid)
        {
            var missionapplication = new MissionApplication();
            missionapplication.UserId = userid;
            missionapplication.MissionId = missionid;
            missionapplication.AppliedAt = DateTime.Now;
            missionapplication.ApprovalStatus = "1";
            _db.Add(missionapplication);
            _db.SaveChanges();
        }
        public void adduser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }
        public void userRating(MissionRating rating)
        {
            _db.MissionRatings.Update(rating);
            _db.SaveChangesAsync();
        }

        public void userComment(Comment comment)
        {
            _db.Comments.Add(comment);
            _db.SaveChanges();
        }


        public List<FavoriteMission> favoriteMissions()
        {
            return _db.FavoriteMissions.ToList();
        }

        public List<MissionApplication> applications()
        {
            return _db.MissionApplications.ToList();
        }
        public List<Comment> comments()
        {
            return _db.Comments.ToList();
        }

        public void passwordReset(PasswordReset pswdreset)
        {
            _db.PasswordResets.Add(pswdreset);
            _db.SaveChanges();
        }
        public void AddUserSkills(long SkillId, int UserId)
        {
            UserSkill skill = new UserSkill();
            skill.SkillId = SkillId;
            skill.UserId = UserId;
            _db.UserSkills.Add(skill);
            _db.SaveChanges();
        }
        public void updateuser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public ContactU addContactUs(string subject, string message, string FirstName, string Surname, string email)
        {
            var contactUs = new ContactU();
            contactUs.FirstName = FirstName;
            contactUs.LastName = Surname;
            contactUs.Email = email;
            contactUs.Subject = subject;
            contactUs.Message = message;
            contactUs.CreatedAt = DateTime.Now;

            _db.Add(contactUs);
            _db.SaveChanges();
            return contactUs;
        }
        public User UserExist(string Email)
        {
            return _db.Users.FirstOrDefault(u => u.Email == Email);
        }
    }
}

