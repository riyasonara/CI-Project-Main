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
            // _Iuser = Iuser;
        }
        public IActionResult UserCrud()
        {
            var uservm = new UserCrudVieweModel();
            uservm.users = _db.Users.ToList();
            return View(uservm);
        }

        public IActionResult CmsPage() {

            var uservm = new UserCrudVieweModel();
            uservm.pages = _db.CmsPages.ToList();
            return View(uservm);

        
        }
        public IActionResult Missions() {

            var mission = new UserCrudVieweModel();
            mission.missions = _db.Missions.ToList();
            return View(mission);

        
        }
        
        public IActionResult MissionApplication() {

            var missionApplication = new UserCrudVieweModel();
            missionApplication.missions = _db.Missions.ToList();
            missionApplication.users = _db.Users.ToList();
            missionApplication.MissionApplications = _db.MissionApplications.ToList();
            return View(missionApplication);

        
        }
        public IActionResult AdminStory()
        {
            var story = new UserCrudVieweModel();
            story.missions = _db.Missions.ToList();
            story.users = _db.Users.ToList();
            story.Stories = _db.Stories.ToList();
            return View(story);

        }

    }
}
