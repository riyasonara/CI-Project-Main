using CI_PLatform_Entities.CIDbContext;
using CI_PLatform_Entities.Models;
using Microsoft.AspNetCore.Mvc;
using CI_PLatform_Entities.CIDbContext;
using CI_PLatform_Entities.Models;
using CI_Platform_Project.Models;
using Microsoft.AspNetCore.Mvc;


namespace CI_Platform_Project.Controllers
{
    public class StoryListingController : Controller
    {

        private readonly CiPlatformContext _db;

        public StoryListingController(CiPlatformContext db)
        {
            _db = db;
        }
        public IActionResult StoryListing(int? page)
        {
            List<Story> storylist = _db.Stories.ToList();
            List<VolunteeringViewModel> StoryList = new List<VolunteeringViewModel>();
            foreach (var story in storylist)
            {
                var storyuser = _db.Users.FirstOrDefault(x => x.UserId == story.UserId);
                StoryList.Add(new VolunteeringViewModel
                {
                    StoryId = story.StoryId,
                    MissionId = story.MissionId,
                    UserId = story.UserId,
                    StoryTitle = story.Title,
                    ShortDescription = story.Description,
                    username = storyuser.FirstName,
                    lastname = storyuser.LastName,

                });

            }
            ViewBag.StoryList = StoryList;

            int pageSize = 3;
            int skip = (page ?? 0) * pageSize;
            var Missions = StoryList.Skip(skip).Take(pageSize).ToList();
            int totalMissions = StoryList.Count();
            ViewBag.TotalMission = totalMissions;
            ViewBag.TotalPages = (int)Math.Ceiling(totalMissions / (double)pageSize);
            ViewBag.CurrentPage = page ?? 0;
            return View(Missions);


        }
        public IActionResult ShareStory()
        {
            return View();
        }
    }
}
