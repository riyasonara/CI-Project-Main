using CI_PLatform_Entities.CIDbContext;
using CI_PLatform_Entities.Models;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Diagnostics;
using System.Net.Mail;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Microsoft.AspNetCore.Http.Extensions;
using CI_Platform_Project.Models;
using CI_Project.Repository.Interface;

namespace CI_Platform_Project.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class ForgotPasswordController : Controller
    {
        private readonly CiPlatformContext _db;
        private readonly IUserInterface _Iuser;

        public ForgotPasswordController(CiPlatformContext db, IUserInterface Iuser)
        {
            _db = db;
            _Iuser = Iuser;
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]

        public IActionResult ForgotPassword(ForgotPasswordViewModel user)
        {
            if (ModelState.IsValid)
            {
                var status = _Iuser.UserByEmail(user.Email);
                if (status == null)
                {
                    ViewBag.EmailError = "Email Does not exist";
                    return View();
                }
                else
                {
                    /*Generate Token*/
                    var finalString = Guid.NewGuid().ToString();
                    /*Update Password Reset Table*/
                    PasswordReset entry = new PasswordReset();
                    entry.Email = user.Email;
                    entry.Token = finalString;
                    _Iuser.passwordReset(entry);
                    HttpContext.Session.SetString("token_session", finalString);


                    /* Sending Mail */
                    var mailbody = "<h3>Click link to reset password</h3><br><h4><a href='" + "https://localhost:44341/ForgotPassword/ResetPassword?token=" + finalString + "'>Reset Password</a></h4>";

                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse("rahulshah89098@gmail.com"));
                    email.To.Add(MailboxAddress.Parse(user.Email));
                    email.Subject = "Reset Your Password";
                    email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mailbody };

                    using var smtp = new SmtpClient();
                    smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate("rahulshah89098@gmail.com", "aufeorkjihnoevvs");
                    smtp.Send(email);
                    smtp.Disconnect(true);
                    /*#endregion Send Mail*/
                    TempData["okay"] = "Reset link has been sent to your registered Email-id";
                    return RedirectToAction("ForgotPassword", "ForgotPassword");
                }
            }
            return View();

        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(User model)
        {
            //if (ModelState.IsValid)
            //{
            string token = HttpContext.Session.GetString("token_session");
            var validtoken = _Iuser.passwordreset().Where(m => m.Token == token).FirstOrDefault();
            if (validtoken != null)
            {
                var user = _Iuser.users().FirstOrDefault(x => x.Email == validtoken.Email);
                if (model.Password != null)
                {
                    user.Password = model.Password;

                    _db.Users.Update(user);
                    _db.SaveChanges();

                    //HttpContext.Session.Remove("token_session");

                    TempData["okay"] = "Your password has been changed";
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return View();
                }


            }
            else
            {
                return View();
            }
        }
    }
}
