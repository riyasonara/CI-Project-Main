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
            var uservm = new UserCrudVieweModel();
            uservm.users = _db.Users.ToList();
            return View(uservm);
        }

        public IActionResult CmsPage() {

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

        [HttpGet]
        public bool approveMission(long missionApplicationId) 
        {
            var missionapp = _db.MissionApplications.FirstOrDefault( m => m.MissionApplicationId == missionApplicationId);
            missionapp.ApprovalStatus = "Approved";

            _db.MissionApplications.Update(missionapp);
            _db.SaveChanges();

            return true;
        }

        [HttpGet]

        public bool rejectMission(long missionApplicationId)
        {
            var missionrej = _db.MissionApplications.FirstOrDefault(m => m.MissionApplicationId == missionApplicationId);
            missionrej.ApprovalStatus = "Rejected";

            _db.MissionApplications.Update(missionrej);
            _db.SaveChanges();

            return true;
        }
    }
}
