using CI_PLatform_Entities.CIDbContext;
using CI_PLatform_Entities.Models;
using Microsoft.AspNetCore.Mvc;
using CI_PLatform_Entities.CIDbContext;
using CI_PLatform_Entities.Models;
using CI_Platform_Project.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Mail;
using MailKit.Security;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using CI_Project.Repository.Interface;
using System.Web;

namespace CI_Platform_Project.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class StoryListingController : Controller
    {

        private readonly CiPlatformContext _db;
        private readonly IUserInterface _Iuser;

        public StoryListingController(CiPlatformContext db, IUserInterface Iuser)
        {
            _db = db;
            _Iuser = Iuser;
        }
        public IActionResult StoryListing(int? page)
        {
            List<Story> storylist = _db.Stories.Where(x=>x.Status!="Draft").ToList();
            List<VolunteeringViewModel> StoryList = new List<VolunteeringViewModel>();
            foreach (var story in storylist)
            {
                var storyuser = _db.Users.FirstOrDefault(x => x.UserId == story.UserId);
                var storymedia = _db.StoryMedia.Where(s => s.StoryId == story.StoryId).FirstOrDefault();
                StoryList.Add(new VolunteeringViewModel
                {
                    StoryId = story.StoryId,
                    MissionId = story.MissionId,
                    UserId = story.UserId,
                    StoryTitle = story.Title,
                    ShortDescription = story.Description,
                    username = storyuser.FirstName,
                    lastname = storyuser.LastName,
                    storypath = storymedia != null ? storymedia.Path : null,

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
        public IActionResult ShareStory(long storyId)
        {
            IEnumerable<Mission> missions = _db.Missions.ToList();
            ViewData["mission"] = _db.MissionApplications.ToList();
            var UserId = Convert.ToInt64(HttpContext.Session.GetString("userID"));

            StoryViewModel storyView = new StoryViewModel();

            if (storyId != 0)
            {
                storyView.storyMedia = _db.StoryMedia.Where(e => e.StoryId == storyId).ToList();
                storyView.applications = _db.MissionApplications.Where(u => u.UserId == UserId).ToList();

                var story = _db.Stories.Where(e => e.StoryId == storyId).FirstOrDefault();
                storyView.MissionId = story.MissionId;
                storyView.Title = story.Title;
                storyView.editor1 = story.Description;
                storyView.CreatedAt = story.CreatedAt;
                storyView.StoryId = storyId;

            }

            return View(storyView);
        }

        [HttpPost]
        public IActionResult ShareStory(StoryViewModel storyView, IFormFileCollection? dragdrop, string action)
        {
            if (action == "submit")
            {
                if (storyView.StoryId == null || storyView.StoryId == 0)
                {
                    IEnumerable<Mission> missions = _db.Missions.ToList();
                    ViewData["mission"] = _db.MissionApplications.ToList();
                    Story story = new Story();
                    story.UserId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
                    story.MissionId = storyView.MissionId;
                    story.Title = storyView.Title;
                    story.Description = storyView.editor1;
                    story.Status = "1";
                    story.CreatedAt = DateTime.Now;

                    foreach (IFormFile file in dragdrop)
                    {
                        if (file != null)
                        {
                            //Set Key Name
                            string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                            //Get url To Save
                            string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\storyuserimg", ImageName);

                            using (var stream = new FileStream(SavePath, FileMode.Create))
                            {
                                StoryMedium sm = new StoryMedium();
                                sm.Type = file.ContentType.ToString().Replace("image/", "");
                                sm.Path = ImageName;
                                sm.CreatedAt = DateTime.Now;
                                story.StoryMedia.Add(sm);
                                file.CopyTo(stream);
                            }
                        }
                    }

                    _db.Stories.Add(story);
                    _db.SaveChanges();
                    return RedirectToAction("StoryListing", "StoryListing");
                }
                else
                {
                    var foundstory = _db.Stories.FirstOrDefault(x => x.StoryId == storyView.StoryId);
                    foundstory.StoryId = storyView.StoryId;
                    foundstory.UserId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
                    foundstory.MissionId = storyView.MissionId;
                    foundstory.Title = storyView.Title;
                    foundstory.Description = storyView.editor1;
                    foundstory.Status = "1";
                    foundstory.CreatedAt = DateTime.Now;
                    foundstory.UpdatedAt = DateTime.Now;
                    _db.Update(foundstory);
                    _db.SaveChanges();
                    return RedirectToAction("StoryListing", "StoryListing");
                }
            }
            else if (action == "save")
            {
                if (storyView.StoryId == null || storyView.StoryId == 0)
                {


                    IEnumerable<Mission> missions = _db.Missions.ToList();
                    ViewData["mission"] = _db.MissionApplications.ToList();
                    Story story = new Story();
                    story.UserId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
                    story.MissionId = storyView.MissionId;
                    story.Title = storyView.Title;
                    story.Description = storyView.editor1;
                    story.Status = "Draft";
                    story.CreatedAt = DateTime.Now;

                    foreach (IFormFile file in dragdrop)
                    {
                        if (file != null)
                        {
                            //Set Key Name
                            string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                            //Get url To Save
                            string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\storyuserimg", ImageName);

                            using (var stream = new FileStream(SavePath, FileMode.Create))
                            {
                                StoryMedium sm = new StoryMedium();
                                sm.Type = file.ContentType.ToString().Replace("image/", "");
                                sm.Path = ImageName;
                                sm.CreatedAt = DateTime.Now;
                                story.StoryMedia.Add(sm);
                                file.CopyTo(stream);
                            }
                        }
                    }

                    _db.Stories.Add(story);
                    _db.SaveChanges();
                    return RedirectToAction("StoryListing", "StoryListing");

                }
                else
                {
                    var foundstory = _db.Stories.FirstOrDefault(x => x.StoryId == storyView.StoryId);
                    foundstory.StoryId = storyView.StoryId;
                    foundstory.UserId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
                    foundstory.MissionId = storyView.MissionId;
                    foundstory.Title = storyView.Title;
                    foundstory.Description = storyView.editor1;
                    foundstory.Status = "Draft";
                    foundstory.CreatedAt = DateTime.Now;
                    foundstory.UpdatedAt = DateTime.Now;
                    _db.Update(foundstory);
                    _db.SaveChanges();
                    return RedirectToAction("DraftStory", "StoryListing");

                }
            }
            else { return View(); }
        }



         //< ====================================================================================== >
         //< =============================== sending mail to co-Worker ============================ >
         //< ====================================================================================== >
        public JsonResult Recommend(string targetURL, string userMail)
        {
            var currentUser = HttpContext.Session.GetString("username");
            var mailbody = "<h3>You got a mission recommendation from " + currentUser + "<br /> Do visit it</h3><h4><a href=" + targetURL + ">Checkout Mission</a></h4>";

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("rahulshah89098@gmail.com"));
            email.To.Add(MailboxAddress.Parse(userMail));
            email.Subject = "Recommend mission";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mailbody };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("rahulshah89098@gmail.com", "aufeorkjihnoevvs");
            smtp.Send(email);
            smtp.Disconnect(true);
            return Json(new { status = 1 });
        }






        public IActionResult StoryDetail(int missionid,long storyid)
            {
            var userId = HttpContext.Session.GetString("userID");
            var story = _db.Stories.Find(storyid);
            return View();
            }
            
        

        public IActionResult DraftStory()
        {

            var SessionUserId = HttpContext.Session.GetString("userID");
            var stories= _db.Stories.Where(u => u.Status == "Draft" && u.UserId == Convert.ToUInt32(SessionUserId)).ToList();

            List<StoryViewModel> storyView = new List<StoryViewModel>();


            foreach (var story in stories)
            {

                var storyuser = _db.Users.FirstOrDefault(x => x.UserId == story.UserId);
                var missiontheme = _db.Missions.FirstOrDefault(m => m.MissionId == story.MissionId).ThemeId;
                var storytheme = _db.MissionThemes.FirstOrDefault(m => m.MissionThemeId == missiontheme).Title;
                var storymedia = _db.StoryMedia.FirstOrDefault(m => m.StoryId == story.StoryId);
                storyView.Add(new StoryViewModel
                {
                    StoryId = story.StoryId,
                    MissionId = story.MissionId,
                    UserId = story.UserId,
                    StoryTitle = story.Title,
                    Themename = storytheme,
                    ShortDescription = HttpUtility.HtmlDecode(story.Description),
                    username = storyuser.FirstName,
                    lastname = storyuser.LastName,
                    //Useravtar = storyuser.Avatar != null ? storyuser.Avatar : "",
                    storymediapath = storymedia != null ? storymedia.Path : "",

                });
            }
            var Storys = storyView;
            ViewBag.StoryList = storyView;
            return View(Storys);
        }


    }
}