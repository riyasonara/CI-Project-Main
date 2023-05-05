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
            return _db.Users.Where(u => u.DeletedAt == null).ToList();
        }
        public List<Mission> missionlist()
        {
            return _db.Missions.Where(m => m.DeletedAt == null).ToList();
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
            return _db.Comments.OrderByDescending(u=>u.CreatedAt).ToList();
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

        public User addUser(string avatar, string firstName, string lastName, string email, string password, string empid, string department, long cityId, long countryId, string ProfileText, int status)
        {
            User user = new User();
            user.Avatar = avatar;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            user.Password = password;
            user.EmployeeId = empid;
            user.Department = department;
            user.CityId = cityId;
            user.CountryId = countryId;
            user.ProfileText = ProfileText;
            user.Status = status;
            
            user.CreatedAt = DateTime.Now;

            _db.Add(user);
            _db.SaveChanges();
            return user;
        }

        public User updateUser(string avatar,string firstName, string lastName, string email, string password, string empid, string department, long cityId, long countryId, string ProfileText, int status, long userId)
        {
            User user = _db.Users.FirstOrDefault(u => u.UserId == userId);
            user.UserId = userId;
            user.Avatar = avatar;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            user.Password = password;
            user.EmployeeId = empid;
            user.Department = department;
            user.CityId = cityId;
            user.CountryId = countryId;
            user.ProfileText = ProfileText;
            user.Status = status;
            user.UpdatedAt = DateTime.Now;

            _db.Update(user);
            _db.SaveChanges();
            return user;
        }
        public void DeleteUser(long userId)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserId == userId);
            user.DeletedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            _db.Update(user);
            _db.SaveChanges();
        }

        public Mission addMission(string Title, string ShortDesc, string Desc, int city, int country, string OrgName, string OrgDetail, string misstype, int seats, DateTime startdate, DateTime endDate, DateTime RegDeadline, string availability, int themeid, int skill)
        {
            var mission = new Mission();
            mission.Title = Title;
            mission.ShortDescription = ShortDesc;
            mission.Description = Desc;
            mission.CityId= city;
            mission.CountryId= country;
            mission.OrganizationName= OrgName;
            mission.OrganizationDetail= OrgDetail;
            mission.MissionType= misstype;
            mission.Availability= availability;
            mission.SeatsLeft= seats;
            mission.Availability = availability;
            mission.Deadline= DateTime.Now;
            mission.StartDate = DateTime.Now;
            mission.EndDate = DateTime.Now;
            mission.ThemeId= themeid;
           


            _db.Add(mission);
            _db.SaveChanges();
            MissionSkill missionSkill = new MissionSkill()
            {
                SkillId = skill,
                MissionId = mission.MissionId,
            };
            _db.MissionSkills.Add(missionSkill);
            _db.SaveChanges();
            return mission;
        }

        public Mission updateMission(long missionId, string Title, string ShortDesc, string Desc, int city, int country, string OrgName, string OrgDetail, string misstype, int seats, DateTime startdate, DateTime endDate, DateTime RegDeadline, string availability, int themeid, int skill)
        {
            Mission mission = _db.Missions.FirstOrDefault(m => m.MissionId ==missionId);
            mission.Title = Title;
            mission.ShortDescription = ShortDesc;
            mission.Description = Desc;
            mission.CityId= city;
            mission.CountryId= country;
            mission.OrganizationName= OrgName;
            mission.OrganizationDetail= OrgDetail;
            mission.MissionType= misstype;
            mission.Availability= availability;
            mission.UpdatedAt = DateTime.Now;

            _db.Update(mission);
            _db.SaveChanges();

            MissionSkill missionSkill = new MissionSkill()
            {
                SkillId = skill,
                MissionId = mission.MissionId,
            };
            _db.MissionSkills.Update(missionSkill);
            _db.SaveChanges();

            return mission;
        }


        public Banner AddBanner(string description, string image, int sortorder)
        {
            Banner banner = new Banner();
            banner.SortOrder = sortorder;
            banner.Image = image;
            banner.Text = description;
            banner.CreatedAt = DateTime.Now;
            _db.Add(banner);
            _db.SaveChanges();
            return banner;
        }
        public Banner UpdateBanner(string description, string image, int sortorder, long bannerId)
        {
            Banner banner = _db.Banners.FirstOrDefault(b => b.BannerId == bannerId);
            banner.SortOrder = sortorder;
            banner.Image = image;
            banner.Text = description;
            banner.UpdatedAt = DateTime.Now;
            _db.Update(banner);
            _db.SaveChanges();
            return banner;
        }
        public List<Banner> AllBanners()
        {
            return _db.Banners.Where(b => b.DeletedAt == null).ToList();
        }
        public void DeleteBanner(long bannerId)
        {
            var banner = _db.Banners.FirstOrDefault(t => t.BannerId == bannerId);
            banner.DeletedAt = DateTime.Now;
            banner.UpdatedAt = DateTime.Now;
            _db.Update(banner);
            _db.SaveChanges();
        }
    }
}

