using CI_PLatform_Entities.CIDbContext;
using CI_PLatform_Entities.Models;
using CI_Platform_Project.Models;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using MailKit.Search;
using System.Globalization;
using Microsoft.Data.SqlClient;
using Org.BouncyCastle.Asn1.X509;
using WebMatrix.WebData;
using System.ComponentModel;
using MailKit.Security;
using MimeKit;
using System.Net.Mail;
using MimeKit;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Diagnostics;
using System.Net.Mail;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using CI_Project.Repository.Interface;
using System.Linq;

namespace CI_Platform_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly CiPlatformContext _db;
        private readonly IUserInterface _Iuser;

        public UserController(CiPlatformContext db, IUserInterface Iuser)
        {
            _db = db;
            _Iuser = Iuser;
        }

        public IActionResult Registration()
        {

            return View();
        }

        public IActionResult Index()
        {

            return View();
        }



        // < ====================================================================================== >
        // < =============================== sending mail to co-Worker ======================------------====== >
        // < ====================================================================================== >
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

       

        public IActionResult VolunteeringMission(long missionid,int? id)
        {
            var SessionUserId = HttpContext.Session.GetString("userID");
            var uId = int.Parse(SessionUserId);
            ViewBag.UserId = uId;
            ViewBag.user = _Iuser.users().FirstOrDefault(e => e.UserId == uId);
            List<VolunteeringViewModel> relatedlist = new List<VolunteeringViewModel>();
            IEnumerable<FavoriteMission> objFav = _Iuser.favoriteMissions();
            IEnumerable<Mission> objMis = _Iuser.missionlist();
            IEnumerable<Comment> objComm = _Iuser.comments();
            IEnumerable<Mission> selected = _Iuser.missionlist().Where(m => m.MissionId == missionid).ToList();
            var applied = (uId != null) ? _Iuser.applications().Any(m => m.UserId == int.Parse(SessionUserId) && m.MissionId == missionid) : false;
            var volmission = _Iuser.missionlist().FirstOrDefault(m => m.MissionId == missionid);
            var theme = _Iuser.themelist().FirstOrDefault(m => m.MissionThemeId == volmission.ThemeId);
            var City = _Iuser.cities().FirstOrDefault(m => m.CityId == volmission.CityId);
            var prevRating = _Iuser.missionRatings().FirstOrDefault(e => e.MissionId == volmission.MissionId && e.UserId == Convert.ToInt64(SessionUserId));
            var themeobjective = _Iuser.goalMissions().FirstOrDefault(m => m.MissionId == missionid);
            string[] Startdate = volmission.StartDate.ToString().Split(" ");
            string[] Enddate = volmission.EndDate.ToString().Split(" ");
            VolunteeringViewModel volunteeringVM = new VolunteeringViewModel();
            volunteeringVM.MissionId = missionid;
            volunteeringVM.Title = volmission.Title;
            volunteeringVM.ShortDescription = volmission.ShortDescription;
            volunteeringVM.OrganizationName = volmission.OrganizationName;
            volunteeringVM.Description = volmission.Description;
            volunteeringVM.OrganizationDetail = volmission.OrganizationDetail;
            volunteeringVM.Availability = volmission.Availability;
            volunteeringVM.MissionType = volmission.MissionType;
            volunteeringVM.Cityname = City.Name;
            volunteeringVM.Themename = theme.Title;
            volunteeringVM.EndDate = Enddate[0];
            volunteeringVM.StartDate = Startdate[0];
            volunteeringVM.isapplied = applied;
            volunteeringVM.UserPrevRating = prevRating != null ? prevRating.Rating : 0;
            var favrioute = (uId != null) ? _Iuser.favoriteMissions().Any(u => u.UserId == Convert.ToInt64(SessionUserId) && u.MissionId == volmission.MissionId) : false;
            //if (prevRating != null) { volunteeringVM.UserPrevRating = prevRating.Rating; }

            volunteeringVM.GoalObjectiveText = themeobjective.GoalObjectiveText;
            ViewBag.Missiondetail = volunteeringVM;



            var relatedmission = _Iuser.missionlist().Where(m => m.ThemeId == volmission.ThemeId && m.MissionId != missionid).ToList();
            foreach (var item in relatedmission.Take(3))
            {

                var relcity = _Iuser.cities().FirstOrDefault(m => m.CityId == item.CityId);
                var reltheme = _Iuser.themelist().FirstOrDefault(m => m.MissionThemeId == item.ThemeId);
                var relgoalobj = _Iuser.goalMissions().FirstOrDefault(m => m.MissionId == item.MissionId);
                string[] Startdate1 = item.StartDate.ToString().Split(" ");
                string[] Enddate2 = item.EndDate.ToString().Split(" ");



                relatedlist.Add(new VolunteeringViewModel
                {
                    MissionId = item.MissionId,
                    Cityname = relcity.Name,
                    Themename = reltheme.Title,
                    Title = item.Title,
                    ShortDescription = item.ShortDescription,
                    StartDate = Startdate1[0],
                    EndDate = Enddate2[0],
                    Availability = item.Availability,
                    OrganizationName = item.OrganizationName,
                    GoalObjectiveText = relgoalobj.GoalObjectiveText,
                    MissionType = item.MissionType,
                    isfav = favrioute,

                }
                );
                ;
            }

            
            //=============================== sending mail to co-Worker =======================

            List<User> alluser = _Iuser.users();
            List<VolunteeringViewModel> allavailuser = new List<VolunteeringViewModel>();
            foreach (var all in alluser)
            {
                allavailuser.Add(new VolunteeringViewModel
                {
                    username = all.FirstName,
                    lastname = all.LastName,
                    userEmail = all.Email,
                    UserId = all.UserId,
                });

            }
            ViewBag.allavailuser = allavailuser;





            ViewBag.relatedmission = relatedlist.Take(3);

            List<VolunteeringViewModel> recentvolunteredlist = new List<VolunteeringViewModel>();

            var recentvoluntered = from U in _Iuser.users() join MA in _Iuser.applications() on U.UserId equals MA.UserId where MA.MissionId == missionid select U;
            foreach (var item in recentvoluntered)
            {


                recentvolunteredlist.Add(new VolunteeringViewModel
                {
                    username = item.FirstName,
                });

            }
            ViewBag.recentvolunteered = recentvolunteredlist;

            return View(selected);
        }
        public async Task<IActionResult> apply(long MissionId, long UserId)
        {
            _Iuser.apply(MissionId, UserId);

            return Json(new { success = true });
        }


        public IActionResult NoMissionFound()
        {
            return View();
        }
        public JsonResult AddToFav(int missionId, int id)
        {
            var obj = new FavoriteMission();
            obj.UserId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
            obj.MissionId = missionId;
            obj.CreatedAt = DateTime.Now;

            var isFav = _Iuser.favoriteMissions().FirstOrDefault(m => m.MissionId == missionId && m.UserId == id);
            if (isFav == null)
            {

                _Iuser.favoriteMissions().Add(obj);
            }
            else
            {
                _Iuser.favoriteMissions().Remove(isFav);
            }

            _db.SaveChanges();
            return Json(new { success = true, isFav });
        }

        public IActionResult PostComment(int missionId, string commentVal)
        {
            Comment objComment = new Comment();
            objComment.UserId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
            objComment.MissionId = missionId;
            objComment.CommentValue = commentVal;
            objComment.CreatedAt = DateTime.Now;
            _Iuser.userComment(objComment);


            return RedirectToAction("VolunteeringMission", new { id = Convert.ToInt64(HttpContext.Session.GetString("userID")), missionid = missionId });



        }

        [HttpPost]
        public async Task<IActionResult> AddRating(string rating, long id, long missionId)
        {
            MissionRating ratingExists = _Iuser.missionRatings().FirstOrDefault(fm => fm.UserId == id && fm.MissionId == missionId);

            if (ratingExists != null)
            {
                ratingExists.Rating = int.Parse(rating);
                ratingExists.UserId = id;
                ratingExists.MissionId = missionId;
                _db.MissionRatings.Update(ratingExists);
                await _db.SaveChangesAsync();

                //_Iuser.userRating(ratingExists);
                //return Json(new { success = true, ratingExists, isRated = true });

            }
            else
            {
                var newRating = new MissionRating();
                newRating.Rating = int.Parse(rating);
                newRating.UserId = id;
                newRating.MissionId = missionId;
                await _db.MissionRatings.AddAsync(newRating); await _db.SaveChangesAsync();
                //return Json(new { success = true, newRating, isRated = true });
            }
            return RedirectToAction("VolunteeringMission", "User", new { missionId = missionId });
        }
        public IActionResult landingPage(long userId, int id, int missionid, string? search, int? pageIndex, string? sortValue, string[] country, string[] city, string[] theme)
        {
            var SessionUserId = HttpContext.Session.GetString("userID");
            //var UserId = HttpContext.Session.GetString("userID");
            ViewBag.countrylist = _Iuser.countrylist();
            ViewBag.themelist = _Iuser.themelist();
            ViewBag.citylist = _Iuser.cities();
            ViewBag.skilllist = _Iuser.skilllist();
            return View();

        }
        //#region landingpage
        //public ActionResult landingPage(int? page, string searchQuery, int Order)
        //{



        //    var mission = _Iuser.missionlist().ToList();
        //    switch (Order)
        //    {
        //        case 2:
        //            mission = mission.OrderByDescending(e => e.StartDate).ToList();
        //            break;
        //        case 3:
        //            mission = mission.OrderBy(e => e.StartDate).ToList();
        //            break;
        //        case 4:
        //            mission = mission.OrderBy(e => int.Parse(e.Availability)).ToList();
        //            break;
        //        case 5:
        //            mission = mission.OrderByDescending(e => int.Parse(e.Availability)).ToList();
        //            break;
        //        case 6:
        //            mission = mission.OrderBy(e => e.EndDate).ToList();
        //            break;
        //        default:
        //            mission = mission.OrderBy(e => e.Theme).ToList();
        //            break;


        //    }

        //    List<Mission> missions = _Iuser.missionlist();
        //    List<City> city = _Iuser.cities();
        //    List<Country> country = _Iuser.countrylist();
        //    List<MissionTheme> theme = _Iuser.themelist();
        //    List<Skill> skill = _Iuser.skilllist();
        //    List<GoalMission> goalMissions = _Iuser.goalMissions();
        //    ViewBag.Goal1 = goalMissions;
        //    ViewBag.citylist = city;
        //    ViewBag.countrylist = country;
        //    ViewBag.themelist = theme;
        //    ViewBag.skilllist = skill;


        //    if (!string.IsNullOrEmpty(searchQuery))
        //    {
        //        mission = _Iuser.missionlist().Where(m => m.Title.Contains(searchQuery)).ToList();
        //        ViewBag.Searchinput = Request.Query["searchQuery"];


        //        if (mission.Count() == 0)
        //        {
        //            return RedirectToAction("NoMissionFound", "User");
        //        }
        //        int pageSize = 6;
        //        int skip = (page ?? 0) * pageSize;
        //        var Missions = mission.Skip(skip).Take(pageSize).ToList();
        //        int totalMissions = mission.Count();
        //        ViewBag.TotalMission = totalMissions;
        //        ViewBag.TotalPages = (int)Math.Ceiling(totalMissions / (double)pageSize);
        //        ViewBag.CurrentPage = page ?? 0;
        //        return View(Missions);
        //    }

        //    else
        //    {
        //        int pageSize = 6;
        //        int skip = (page ?? 0) * pageSize;
        //        var Missions = mission.Skip(skip).Take(pageSize).ToList();
        //        int totalMissions = mission.Count();
        //        ViewBag.TotalMission = totalMissions;
        //        ViewBag.TotalPages = (int)Math.Ceiling(totalMissions / (double)pageSize);
        //        ViewBag.CurrentPage = page ?? 0;
        //        return View();
        //    }

        //}
        //#endregion

        #region Filters
        public IActionResult navbarfilters(long userId, int id, int missionid, string? search, int? pageIndex, string? sortValue, string[] country, string[] city, string[] theme)
        {
            var SessionUserId = HttpContext.Session.GetString("userID");

            List<User> alluser = _Iuser.users();
            List<VolunteeringViewModel> allavailuser = new List<VolunteeringViewModel>();
            foreach (var all in alluser)
            {
                allavailuser.Add(new VolunteeringViewModel
                {
                    username = all.FirstName,
                    lastname = all.LastName,
                    userEmail = all.Email,
                    UserId = all.UserId,
                });

            }
            ViewBag.allavailuser = allavailuser;
            List<Mission> allmission = _Iuser.missionlist();
            List<VolunteeringViewModel> mission = new List<VolunteeringViewModel>();
            foreach (Mission item in allmission)
            {
                var City = _Iuser.cities().FirstOrDefault(u => u.CityId == item.CityId);
                var Country = _Iuser.countrylist().FirstOrDefault(u => u.CountryId == item.CountryId);
                var Theme = _Iuser.themelist().FirstOrDefault(u => u.MissionThemeId == item.ThemeId);
                var goalobj = _Iuser.goalMissions().FirstOrDefault(m => m.MissionId == item.MissionId);
                string[] Startdate1 = item.StartDate.ToString().Split(" ");
                string[] Enddate2 = item.EndDate.ToString().Split(" ");
                var favrioute = (id != null) ? _Iuser.favoriteMissions().Any(u => u.UserId == Convert.ToInt64(SessionUserId) && u.MissionId == item.MissionId) : false;
                var Applybtn = (id != null) ? _Iuser.applications().Any(u => u.MissionId == item.MissionId && u.UserId == Convert.ToInt64(SessionUserId)) : false;
                ViewBag.FavoriteMissions = favrioute;
                var ratiing = _Iuser.missionRatings().Where(u => u.MissionId == item.MissionId).ToList();


                int finalrating = 0;
                if (ratiing.Count > 0)
                {
                    int rating = 0;
                    foreach (var r in ratiing)
                    {

                        rating = rating + r.Rating;


                    }
                    finalrating = rating / ratiing.Count();

                }




                mission.Add(new VolunteeringViewModel
                {
                    MissionId = item.MissionId,
                    Cityname = City.Name,
                    Countryname = Country.Name,
                    Themename = Theme.Title,
                    Title = item.Title,
                    ShortDescription = item.ShortDescription,
                    StartDate = Startdate1[0],
                    EndDate = Enddate2[0],
                    Availability = item.Availability,
                    OrganizationName = item.OrganizationName,
                    GoalObjectiveText = goalobj.GoalObjectiveText,
                    MissionType = item.MissionType,
                    AverageRating = finalrating,
                    isfav = favrioute,
                    isapplied = Applybtn,
                    UserId = Convert.ToInt64(SessionUserId),
                });
            }

            var Missions = mission.ToList();


            //Seacrh
            if (search != null)
            {
                Missions = Missions.Where(m => m.Title.ToUpper().Contains(search.ToUpper())).ToList();

            }

            ////Sort By
            ViewBag.sort = sortValue;
            switch (sortValue)
            {

                case "Newest":
                    Missions = Missions.OrderByDescending(m => m.StartDate).ToList();
                    ViewBag.sortby = "Newest";
                    break;
                case "Oldest":
                    Missions = Missions.OrderBy(m => m.StartDate).ToList();
                    ViewBag.sortby = "Oldest";
                    break;
                case "Lowest seats":
                    Missions = Missions.OrderBy(m => int.Parse(m.Availability)).ToList();
                    break;
                case "Highest seats":
                    Missions = Missions.OrderByDescending(m => int.Parse(m.Availability)).ToList();
                    break;
                case "My favourites":
                    Missions = Missions.Where(m => m.isfav == true).ToList();
                    break;
                case "Registration deadline":
                    Missions = Missions.OrderBy(m => m.EndDate).ToList();
                    break;
            }

            //filter
            if (country.Length > 0)
            {
                Missions = Missions.Where(s => country.Contains(s.Countryname)).ToList();
            }
            if (city.Length > 0)
            {
                Missions = Missions.Where(s => city.Contains(s.Cityname)).ToList();
            }
            if (theme.Length > 0)
            {
                Missions = Missions.Where(s => theme.Contains(s.Themename)).ToList();
            }

            //Pagination
            //int pageSize = 6;
            //int skip = (pageIndex ?? 0) * pageSize;
            //var Missionss = Missions.Skip(skip).Take(pageSize).ToList();
            //int totalMissions = mission.Count();

            //ViewBag.TotalMission = totalMissions;
            //ViewBag.TotalPages = (int)Math.Ceiling(totalMissions / (double)pageSize);
            //ViewBag.CurrentPage = pageIndex ?? 0;
            var missionfinal = Missions;


            return PartialView("_LandingCards", missionfinal);
        }


        #endregion


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "User");
        }




        [HttpPost]

        public IActionResult Index(User user)
        {
            var status = _Iuser.UserByEmail_Password(user.Email, user.Password);
            if (status != null)
            {
                var username = _Iuser.UserByEmail(user.Email);
                HttpContext.Session.SetString("userID", username.UserId.ToString());
                HttpContext.Session.SetString("username", username.FirstName);
                TempData["okay"] = "loggedin successfully";
                return RedirectToAction("LandingPage", new { @id = status.UserId });
            }
            else
            {
                ViewBag.LoginError = "Login Credentials Invalid";
                return View();
            }


        }

        [HttpPost]

        public IActionResult Registration(RegisterVM user)
        {
            if (ModelState.IsValid)
            {
                var obj = _Iuser.UserByEmail(user.Email);
                if (obj == null)
                {
                    byte[] timestamp = BitConverter.GetBytes(DateTime.Now.Ticks);
                    User newUser = new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        Password = user.Password,
                        CreatedAt = timestamp
                    };
                    _Iuser.adduser(newUser);
                    TempData["okay"] = "Registered successfully";
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ViewBag.RegistrationError = "Email already exists";
                }
            }
            return View();

        }

        [HttpGet]
        public IActionResult userProfile()
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString("userID"));

            var user = _Iuser.users().FirstOrDefault(u => u.UserId == userId);
            UserProfileViewModel userProfile = new UserProfileViewModel();

            userProfile.employeeID = user.EmployeeId;
            userProfile.FirstName = user.FirstName;
            userProfile.Surname = user.LastName;
            userProfile.Email = user.Email;

            userProfile.WhyIVolunteer = user.WhyIVolunteer;
            userProfile.title = user.Title;
            userProfile.CityId = user.CityId;
            userProfile.CountryId = user.CountryId;
            //userVM.availability = user.
            userProfile.ProfileText = user.ProfileText;
            userProfile.LinkedInUrl = user.LinkedInUrl;
            userProfile.Avatar = user.Avatar != null ? user.Avatar : "";
            userProfile.department = user.Department;
            userProfile.CityId = user.CityId;
            userProfile.CountryId = user.CountryId;
            userProfile.Availability = user.Availability;
            var allskills = _Iuser.skilllist();
            ViewBag.allskills = allskills;
            var skills = from US in _db.UserSkills
                         join S in _db.Skills on US.SkillId equals S.SkillId
                         select new { US.SkillId, S.SkillName, US.UserId };
            var uskills = skills.Where(e => e.UserId == userId).ToList();
            ViewBag.userskills = uskills;
            foreach (var skill in uskills)
            {
                var rskill = allskills.FirstOrDefault(e => e.SkillId == skill.SkillId);
                allskills.Remove(rskill);
            }
            ViewBag.remainingSkills = allskills;
            ViewBag.allcities = _Iuser.cities();
            ViewBag.allcountry = _Iuser.countrylist();
            userProfile.skills = _Iuser.skilllist();
            userProfile.userSkills = _Iuser.skilllist(Convert.ToInt32(userId));

            var r = from row1 in userProfile.skills.AsEnumerable()
                    where !userProfile.userSkills.AsEnumerable().Select(r => r.SkillId).Contains(row1.SkillId)
                    select row1;

            userProfile.RemainingSkill = r.ToList();
            return View(userProfile);
        }

        [HttpPost]

        public async Task<IActionResult> userProfile(UserProfileViewModel model, IFormFileCollection files)
        {

            var userId = Convert.ToInt64(HttpContext.Session.GetString("userID"));

            //long id = Convert.ToInt64(userid);
            //long storyid = model.storyId;
            var userdetail = _Iuser.users().FirstOrDefault(u => u.UserId == userId);
            userdetail.FirstName = model.FirstName;
            userdetail.LastName = model.Surname;
            userdetail.WhyIVolunteer = model.WhyIVolunteer;
            userdetail.Title = model.title;

            userdetail.EmployeeId = model.employeeID;
            userdetail.ProfileText = model.ProfileText;
            userdetail.LinkedInUrl = model.LinkedInUrl;
            userdetail.UpdatedAt = DateTime.Now;
            userdetail.Department = model.department;
            userdetail.CountryId = model.CountryId;
            userdetail.CityId = model.CityId;
            userdetail.Availability = model.Availability;

            if (files.Count() == 0)
            {
                model.Avatar = userdetail.Avatar;
            }
            else
            {
                userdetail.Avatar = model.Avatar;

            }
            foreach (var file in files)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var imageBytes = ms.ToArray();
                    var base64String = Convert.ToBase64String(imageBytes);
                    userdetail.Avatar = "data:image/png;base64," + base64String;
                    model.Avatar = "data:image/png;base64," + base64String;
                    HttpContext.Session.SetString("useravtar", "data:image/png;base64," + base64String);
                }
            }

            var allskills = _Iuser.skilllist();
            ViewBag.allskills = allskills;
            var skills = from US in _db.UserSkills
                         join S in _db.Skills on US.SkillId equals S.SkillId
                         select new { US.SkillId, S.SkillName, US.UserId };
            var uskills = skills.Where(e => e.UserId == userId).ToList();
            ViewBag.userskills = uskills;
            foreach (var skill in uskills)
            {
                var rskill = allskills.FirstOrDefault(e => e.SkillId == skill.SkillId);
                allskills.Remove(rskill);
            }
            ViewBag.remainingSkills = allskills;
            ViewBag.allcities = _Iuser.cities();
            ViewBag.allcountry = _Iuser.countrylist();

            _db.Users.Update(userdetail);
            _db.SaveChanges();
            //_Iuser.updateuser(userdetail);
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> SaveUserSkills(long[] selectedSkills)
        {
            var userid = HttpContext.Session.GetString("userID");
            long id = Convert.ToInt64(userid);
            var abc = _db.UserSkills.Where(e => e.UserId == id).ToList();
            _db.RemoveRange(abc);
            _db.SaveChanges();
            foreach (var skills in selectedSkills)
            {
                _Iuser.AddUserSkills(skills, Convert.ToInt32(userid));
            }
            return RedirectToAction("userprofile","user");
        }

        public IActionResult PrivacyPolicy()
        {
            return View();
        }

        //Timesheet
        public IActionResult VolunteeringTimeSheet()
        {
            StoryViewModel ss = new StoryViewModel();
            var userid = HttpContext.Session.GetString("userID");
            ss.missions = _db.Missions.ToList();
            ss.applications = _db.MissionApplications.Where(e => e.UserId == Convert.ToInt64(userid)).ToList();
            ss.timesheets = _db.Timesheets.ToList();

            return View(ss);
        }

        [HttpPost]
        public IActionResult VolunteeringTimeSheet(StoryViewModel ss)
        {
            if (ss.TimesheetId != 0)
            {
                Timesheet sheet = _db.Timesheets.FirstOrDefault(e => e.TimesheetId == ss.TimesheetId);
                if (ss.hour != 0 && ss.minute != 0)
                {
                    var userId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
                    //var user = Convert.ToInt32(userid);
                    sheet.UserId = userId;
                    sheet.MissionId = ss.MissionId;
                    sheet.TimesheetTime = ss.hour + ":" + ss.minute;
                    ss.DateVolunteered = ss.DateVolunteered;
                    sheet.DateVolunteered = ss.DateVolunteered;
                    sheet.Notes = ss.Notes;
                }

                else
                {
                    var userId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
                    //var user = Convert.ToInt32(userid);
                    sheet.UserId = userId;
                    sheet.MissionId = ss.MissionId;
                    ss.DateVolunteered = ss.DateVolunteered;
                    sheet.DateVolunteered = ss.DateVolunteered;
                    sheet.Notes = ss.Notes;
                    sheet.Action = ss.Action;
                }

                _db.Timesheets.Update(sheet);
                _db.SaveChanges();
            }

            else
            {
                Timesheet sheets = new Timesheet();
                if (ss.hour != 0 && ss.minute != 0)
                {
                    var userId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
                    //var user = Convert.ToInt32(userid);
                    sheets.UserId = userId;
                    sheets.MissionId = ss.MissionId;
                    sheets.TimesheetTime = ss.hour + ":" + ss.minute;
                    ss.DateVolunteered = ss.DateVolunteered;
                    sheets.DateVolunteered = ss.DateVolunteered;
                    sheets.Notes = ss.Notes;
                }

                else
                {
                    var userId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
                    //var user = Convert.ToInt32(userid);
                    sheets.UserId = userId;
                    sheets.MissionId = ss.MissionId;
                    ss.DateVolunteered = ss.DateVolunteered;
                    sheets.DateVolunteered = ss.DateVolunteered;
                    sheets.Notes = ss.Notes;
                    sheets.Action = ss.Action;
                }

                _db.Timesheets.Add(sheets);
                _db.SaveChanges();
            }



            return RedirectToAction("VolunteeringTimeSheet", "User");
        }

        public IActionResult EditTimesheet(int id)
        {
            StoryViewModel ss = new StoryViewModel();
            var userid = HttpContext.Session.GetString("userID");
            ss.missions = _db.Missions.ToList();
            ss.applications = _db.MissionApplications.Where(e => e.UserId == Convert.ToInt64(userid)).ToList();
            ss.timesheets = _db.Timesheets.ToList();

            ss.Singlesheet = _db.Timesheets.FirstOrDefault(u => u.UserId == Convert.ToInt64(userid));
            var sheet = _db.Timesheets.FirstOrDefault(u => u.TimesheetId == id);

            ss.DateVolunteered = sheet.DateVolunteered;
            ss.TimesheetId = sheet.TimesheetId;
            ss.Notes = sheet.Notes;

            //if (ss.hour != 0 && ss.minute != 0)
            if (sheet.TimesheetTime != null)
            {
                ss.hour = Convert.ToInt32(sheet.TimesheetTime.Split(":")[0]);
                ss.minute = Convert.ToInt32(sheet.TimesheetTime.Split(":")[1]);
            }

            else
            {
                //ss.hour = 0;
                //ss.minute = 0;
                ss.Action = sheet.Action;
            }


            return View("VolunteeringTimeSheet", ss);
        }


        //Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var time = _db.Timesheets.FirstOrDefault(x => x.TimesheetId == id);
            _db.Timesheets.Remove(time);
            _db.SaveChanges();

            return View();
        }
    }
}
