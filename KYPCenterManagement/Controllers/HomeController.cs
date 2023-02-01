using KYPCenterManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KYPCenterManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly KYPCenterManagementEntities _dbContext = new KYPCenterManagementEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                bool IsValidUser = _dbContext.Users
               .Any(u => u.Username.ToLower() == user
               .Username.ToLower() && u
               .Password == user.Password);

                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "Centers");
                }
            }
            ModelState.AddModelError("", "Invalid Username or Password");
            return View();
        }
        //public ActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Register(User registerUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _dbContext.Users.Add(registerUser);
        //        _dbContext.SaveChanges();
        //        return RedirectToAction("Login");

        //    }
        //    return View();
        //}
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}