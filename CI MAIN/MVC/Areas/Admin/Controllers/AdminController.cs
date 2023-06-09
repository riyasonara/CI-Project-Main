﻿using CI_PLatform_Entities.CIDbContext;
using CI_PLatform_Entities.Models;
using CI_Platform_Project.Models;
using CI_Project.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using System.Reflection;

namespace CI_Platform_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly CiPlatformContext _db;
        private readonly IUserInterface _Iuser;

        public AdminController(CiPlatformContext db, IUserInterface Iuser)
        {
            _db = db;
            _Iuser = Iuser;
        }
        public IActionResult UserCrud()
        {
            HttpContext.Session.SetInt32("Nav", 1);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var uservm = new UserCrudVieweModel();
            uservm.users = _db.Users.Where(u => u.DeletedAt == null).ToList();
            uservm.Cities = _db.Cities.ToList();
            uservm.Countries = _db.Countries.ToList();
            return View(uservm);
        }


        //Cascading for city and country
        public JsonResult filterCity(long missionCountry)
        {
            IList<City> cities = _db.Cities.Where(m => m.CountryId == missionCountry).ToList();
            return Json(cities);
        }

        [HttpPost]
        public IActionResult adduser(string avatar, string Fname, string Lname, string email, string password, string empID, string department, long city, long country, string proftxt, int status, long userID)
        {
            try
            {
                if (userID == 0 || userID == null)
                {
                    _Iuser.addUser(avatar, Fname, Lname, email, password, empID, department, city, country, proftxt, status);
                }
                else
                {
                    _Iuser.updateUser(avatar, Fname, Lname, email, password, empID, department, city, country, proftxt, status, userID);
                }
                return RedirectToAction("UserCrud");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "User", new { area = "Employee" });
            }



        }

        public IActionResult getUser(long UserID)
        {
            var userlist = _db.Users.FirstOrDefault(x => x.UserId == UserID);
            var user = new UserCrudVieweModel();
            user.avatar = userlist.Avatar;
            user.FirstName = userlist.FirstName;
            user.LastName = userlist.LastName;
            user.Email = userlist.Email;
            user.Password = userlist.Password;
            user.Department = userlist.Department;
            user.ProfileText = userlist.ProfileText;
            user.Status = userlist.Status;
            user.EmployeeId = userlist.EmployeeId;
            user.CityId = userlist.CityId == null ? 0 : userlist.CityId;
            user.CountryId = userlist.CountryId == null ? 0 : userlist.CountryId;
            user.UserId = userlist.UserId;
            user.users = _Iuser.users();
            user.Cities = _db.Cities.ToList();
            user.Countries = _db.Countries.ToList();
            return View("UserCrud", user);
        }

        public IActionResult deleteUser(long UserID)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == UserID);
            user.DeletedAt = DateTime.Now;

            _db.Users.Update(user);
            _db.SaveChanges();

            return RedirectToAction("UserCrud", new { UserID = UserID });
        }

        public IActionResult CmsPage()
        {
            HttpContext.Session.SetInt32("Nav", 2);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var uservm = new UserCrudVieweModel();
            uservm.pages = _db.CmsPages.ToList();
            return View(uservm);


        }


        //CMS

        public IActionResult CMSAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CMSAdd(long CMSId, String Title, String Desc, String Slug, int Status)
        {
            if (CMSId == 0)
            {
                CmsPage cms = new CmsPage()
                {
                    Title = Title,
                    Description = Desc,
                    Slug = Slug,
                    Status = Status,
                    //CreatedAt = DateTime.Now,
                };

                _db.Add(cms);
                _db.SaveChanges();
            }
            else
            {
                CmsPage cms = _db.CmsPages.FirstOrDefault(x => x.CmsPageId == CMSId);
                cms.Status = Status;
                cms.Title = Title;
                cms.Description = Desc;
                cms.Slug = Slug;
                cms.UpdatedAt = DateTime.Now;

                _db.Update(cms);
                _db.SaveChanges();
            }

            return Json("CmsPage");
        }

        /*CMS Get Data*/
        public IActionResult GetCMSData(long CMSId)
        {
            var Data = _db.CmsPages.FirstOrDefault(x => x.CmsPageId == CMSId);
            return Json(Data);
        }

        public IActionResult CMSDelete(long CMSId)
        {
            var cms = _db.CmsPages.FirstOrDefault(u => u.CmsPageId == CMSId);

            _db.CmsPages.Remove(cms);
            _db.SaveChanges();

            return RedirectToAction("CmsPage", "Admin");
        }


        [HttpGet]
        public IActionResult Missions(AdminMissionViewModel mission)
        {

            HttpContext.Session.SetInt32("Nav", 3);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            AdminMissionViewModel missions;
            if (mission.missionId != 0)
            {
                missions = mission;
            }
            else
            {
                missions = new AdminMissionViewModel();
            }

            missions.Missions = _db.Missions.Where(m => m.DeletedAt == null).ToList();
            missions.Cities = _db.Cities.ToList();
            missions.Countries = _db.Countries.ToList();
            missions.MissionThemes = _db.MissionThemes.ToList();
            missions.Skills = _db.Skills.ToList();

            return View(missions);


        }

        [HttpPost]
        public IActionResult addMission(long missionId, string Title, string ShortDesc, string Desc, int city, int country, string OrgName, string OrgDetail, string misstype, int seats, DateTime startdate, DateTime endDate, DateTime RegDeadline, string availability, int themeid, int skill)
        {
            try
            {
                if (missionId == 0 || missionId == null)
                {
                    _Iuser.addMission(Title, ShortDesc, Desc, city, country, OrgName, OrgDetail, misstype, seats, startdate, endDate, RegDeadline, availability, themeid, skill);
                }
                else
                {
                    _Iuser.updateMission(missionId, Title, ShortDesc, Desc, city, country, OrgName, OrgDetail, misstype, seats, startdate, endDate, RegDeadline, availability, themeid, skill);
                }
                return RedirectToAction("Missions");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "User", new { area = "Employee" });
            }

        }

        [HttpPost]
        public IActionResult getMiss(long missionId)
        {
            var missionlist = _db.Missions.FirstOrDefault(x => x.MissionId == missionId);
            var mission = new AdminMissionViewModel();
            mission.Title = missionlist.Title;
            mission.ShortDescription = missionlist.ShortDescription;
            mission.Description = missionlist.Description;
            mission.CityId = missionlist.CityId == null ? 0 : missionlist.CityId;
            mission.CountryId = missionlist.CountryId == null ? 0 : missionlist.CountryId;
            mission.OrganizationName = missionlist.OrganizationName;
            mission.OrganizationDetail = missionlist.OrganizationDetail;
            mission.MissionType = missionlist.MissionType;
            mission.SeatsLeft = missionlist.SeatsLeft;
            mission.Availability = missionlist.Availability;
            mission.missionId = missionlist.MissionId;
            mission.Missions = _Iuser.missionlist();

            return RedirectToAction("Missions", mission);
        }

        public IActionResult delmiss(long missionId)
        {
            var mission = _db.Missions.FirstOrDefault(x => x.MissionId == missionId);
            mission.DeletedAt = DateTime.Now;

            _db.Missions.Update(mission);
            _db.SaveChanges();

            return RedirectToAction("Missions", new { missionId = missionId });
        }


        [HttpPost]
        public IActionResult Upload(IFormFileCollection file)
        {
            return Json(new { success = true });
        }

        public IActionResult MissionApplication()
        {

            HttpContext.Session.SetInt32("Nav", 6);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");

            var missionApplication = new UserCrudVieweModel();
            missionApplication.missions = _db.Missions.ToList();
            missionApplication.users = _db.Users.ToList();
            missionApplication.MissionApplications = _db.MissionApplications.Where(u => u.ApprovalStatus == "Pending").ToList();
            return View(missionApplication);


        }
        public IActionResult AdminStory()
        {

            HttpContext.Session.SetInt32("Nav", 7);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");

            var story = new UserCrudVieweModel();
            story.missions = _db.Missions.ToList();
            story.users = _db.Users.ToList();
            story.Stories = _db.Stories.ToList();
            return View(story);

        }

        [HttpGet]
        public IActionResult approveStory(long StoryId)
        {
            var story =_db.Stories.FirstOrDefault(s=>s.StoryId == StoryId);
            story.Status = "Approved";

            _db.Stories.Update(story);
            _db.SaveChanges();
            return RedirectToAction("AdminStory","Admin");
        }

        [HttpGet]
        public IActionResult rejectStory(long StoryId)
        {
            var storyReject = _db.Stories.FirstOrDefault(s=>s.StoryId ==StoryId);
            storyReject.Status = "Rejected";

            _db.Stories.Update(storyReject); 
            _db.SaveChanges();
            return RedirectToAction("AdminStory", "Admin");
        }


        [HttpGet]
        public IActionResult approveMission(long missionApplicationId)
        {
            var missionapp = _db.MissionApplications.FirstOrDefault(m => m.MissionApplicationId == missionApplicationId);
            var targetMission = _db.Missions.ToList().Where(m => m.MissionId == missionapp.MissionId).FirstOrDefault();
            if (targetMission.Availability != null)
            {
                targetMission.Availability = Convert.ToString(Convert.ToInt32(targetMission.Availability) - 1);
                missionapp.ApprovalStatus = "Approved";
            }

            _db.Missions.Update(targetMission);
            _db.MissionApplications.Update(missionapp);
            _db.SaveChanges();

            return RedirectToAction("MissionApplication", "Admin");
        }

        [HttpGet]

        public IActionResult rejectMission(long missionApplicationId)
        {
            var missionrej = _db.MissionApplications.FirstOrDefault(m => m.MissionApplicationId == missionApplicationId);
            missionrej.ApprovalStatus = "Rejected";

            _db.MissionApplications.Update(missionrej);
            _db.SaveChanges();

            return RedirectToAction("MissionApplication", "Admin");

        }

        [HttpGet]
        public IActionResult MissionTheme(MissionThemeViewModel theme)
        {
            HttpContext.Session.SetInt32("Nav", 4);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            MissionThemeViewModel themes;
            if (theme.MissionThemeId != 0)
            {
                themes = theme;
            }
            else
            {
                themes = new MissionThemeViewModel();
            }

            themes.MissionThemes = _db.MissionThemes.Where(t => t.DeletedAt==null).ToList();

            return View(themes);
        }

        [HttpPost]

        public IActionResult addTheme(string themeName, int Status, long missionThemeId)
        {
            try
            {
                if (missionThemeId == 0 || missionThemeId == null)
                {
                    _Iuser.addtheme(themeName, Status);
                }
                else
                {
                    _Iuser.updateTheme(themeName, Status, missionThemeId);
                }
                return RedirectToAction("MissionTheme");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "User", new { area = "Employee" });
            }
        }


        public IActionResult Themeget(long missionThemeId)
        {
            var themeList = _db.MissionThemes.FirstOrDefault(x => x.MissionThemeId == missionThemeId);
            var theme = new MissionThemeViewModel();

            theme.Title = themeList.Title;
            theme.MissionThemeId = themeList.MissionThemeId;
            theme.Status = themeList.Status;

            return RedirectToAction("MissionTheme", theme);
        }
        public IActionResult deltheme(long missionThemeId)
        {
            var theme = _db.MissionThemes.FirstOrDefault(x => x.MissionThemeId == missionThemeId);
            theme.DeletedAt = DateTime.Now;

            _db.MissionThemes.Update(theme);
            _db.SaveChanges();

            return RedirectToAction("MissionTheme", new { missionThemeId = missionThemeId });
        }

        public IActionResult MissionSkills(AdminSkillViewModel skill)
        {
            var missionskills = new AdminSkillViewModel();
            missionskills.skills = _db.Skills.ToList();

            AdminSkillViewModel skills;
            if (skill.SkillId != 0)
            {
                skills = skill;
            }
            else
            {
                skills = new AdminSkillViewModel();
            }

            skills.skills = _db.Skills.Where(s => s.DeletedAt == null).ToList();


            return View(missionskills);
        }

        [HttpPost]
        public IActionResult addskill(string skillName, int Status, long skillId)
        {
            try
            {
                if (skillId == 0 || skillId == null)
                {
                    _Iuser.addSkill(skillName, Status);
                }
                else
                {
                    _Iuser.updateSkill(skillName, Status, skillId);
                }
                return RedirectToAction("MissionSkills");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "User", new { area = "Employee" });
            }

        }

        public IActionResult skillget(long skillId)
        {
            var skillList = _db.Skills.FirstOrDefault(x => x.SkillId == skillId);
            var skill = new AdminSkillViewModel();

            skill.SkillName = skillList.SkillName;
            skill.SkillId = skillList.SkillId;
            skill.Status = skillList.Status;

            return RedirectToAction("MissionSkills", skill);
        }
        public IActionResult delskill(long skillId)
        {
            var skill = _db.Skills.FirstOrDefault(x => x.SkillId == skillId);
            skill.DeletedAt = DateTime.Now;

            _db.Skills.Update(skill);
            _db.SaveChanges();

            return RedirectToAction("MissionSkills", new { skillId = skillId });
        }




        public IActionResult Banner()
        {
            ViewData["banners"] = _db.Banners.ToList();
            HttpContext.Session.SetInt32("Nav", 8);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            return View();
        }

        [HttpPost]
        public IActionResult Banner(Banner b, IFormFileCollection? bannerPic)
        {
            ViewData["banners"] = _db.Banners.ToList();

            Banner newBan = new Banner();
            newBan.Text = b.Text;

            foreach (IFormFile file in bannerPic)
            {
                if (file != null)
                {
                    string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", ImageName);

                    using (var stream = new FileStream(SavePath, FileMode.Create))
                    {
                        newBan.Image = ImageName;
                        file.CopyTo(stream);
                    }
                }
            }
            newBan.CreatedAt = DateTime.Now;
            _db.Banners.Add(newBan);
            _db.SaveChanges();
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["bye"] = "logged out successfully";
            TempData["okay"] = null;
            return RedirectToAction("LandingPage", "User", new { area = "Employee" });
        }

    }
}
