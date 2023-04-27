using CI_PLatform_Entities.CIDbContext;
using CI_PLatform_Entities.Models;
using CI_Platform_Project.Models;
using CI_Project.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly CiPlatformContext _db;

        public AdminController(CiPlatformContext db, IUserInterface Iuser)
        {
            _db = db;
        }
        public IActionResult UserCrud()
        {
            HttpContext.Session.SetInt32("Nav", 1);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var uservm = new UserCrudVieweModel();
            uservm.users = _db.Users.ToList();
            uservm.Cities = _db.Cities.ToList();
            uservm.Countries = _db.Countries.ToList();
            return View(uservm);
        }

        [HttpPost]

        public IActionResult adduser(long userID, int status, string proftxt, string email, string password, string department, string Fname, string Lname, string empID, int country, int city)
        {


            if (userID == 0)
            {
                User user = new User()
                {
                    FirstName = Fname,
                    LastName = Lname,
                    Email = email,
                    Password = password,
                    Department = department,
                    Status = status,
                    ProfileText = proftxt,
                    EmployeeId = empID,
                    CountryId = country,
                    CityId = city,

                };
                _db.Users.Add(user);
                _db.SaveChanges();
            }
            else
            {
                User user = _db.Users.FirstOrDefault(x => x.UserId == userID);
                user.FirstName = Fname;
                user.LastName = Lname;
                user.Email = email;
                user.Password = password;
                user.Department = department;
                user.CityId = city;
                user.CountryId = country;
                user.Department = department;
                user.ProfileText = proftxt;
                user.EmployeeId = empID;
                user.Status = status;

                _db.Update(user);
                _db.SaveChanges();

            }

            return Json("UserCrud");
        }

        public IActionResult GetUser(long UserID)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == UserID);
            return Json(user);
        }

        public IActionResult deleteUser(long UserID)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == UserID);
            user.DeletedAt = DateTime.Now;

            _db.Users.Update(user);
            _db.SaveChanges();

            return View("UserCrud");
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

            return Json("_CMSAdmin");
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

            return RedirectToAction("Index", "Admin");
        }



        public IActionResult Missions()
        {

            HttpContext.Session.SetInt32("Nav", 3);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");

            var mission = new UserCrudVieweModel();
            mission.missions = _db.Missions.ToList();
            mission.Cities = _db.Cities.ToList();
            mission.Countries = _db.Countries.ToList();
            return View(mission);


        }

        [HttpPost]
        public IActionResult Upload(IFormFileCollection file)
        {
            // Handle the uploaded file here
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
        public IActionResult approveMission(long missionApplicationId)
        {
            var missionapp = _db.MissionApplications.FirstOrDefault(m => m.MissionApplicationId == missionApplicationId);
            missionapp.ApprovalStatus = "Approved";

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
        public IActionResult MissionTheme()
        {
            HttpContext.Session.SetInt32("Nav", 4);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");

            var missiontheme = new UserCrudVieweModel();
            missiontheme.MissionThemes = _db.MissionThemes.ToList();

            return View(missiontheme);
        }

        public IActionResult MissionSkills()
        {
            HttpContext.Session.SetInt32("Nav", 5);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");

            var missionskill = new UserCrudVieweModel();
            missionskill.Skills = _db.Skills.ToList();

            return View(missionskill);
        }
    }
}
